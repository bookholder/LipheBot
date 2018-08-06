using System;
using System.Threading.Tasks;
using LipheBot.Core;
using TwitchLib.Client;
using TwitchLib.Client.Models;
using TwitchLib.Client.Events;
using TwitchLib.Client.Interfaces;




namespace LipheBot.Infra.Twitch
{
    
    public class TwitchChatClient : IChatClient
    {
        
        private readonly ITwitchClient _twitchClient;
        private readonly TwitchClientSettings _settings;
        private  readonly JoinedChannel _joinedChannel;
        
        private  TaskCompletionSource<bool> _connectionCompletionTask = new TaskCompletionSource<bool>();
        private  TaskCompletionSource<bool> _disconnectionCompletionTask = new TaskCompletionSource<bool>();
        private bool _isReady;

        

        public TwitchChatClient(TwitchClientSettings settings)
        {
            _settings = settings;
             _joinedChannel = new JoinedChannel(_settings.TwitchChannel);
             var credentials = new ConnectionCredentials(_settings.TwitchUsername, _settings.TwitchBotOAuth); 
            _twitchClient = new TwitchClient();
            _twitchClient.Initialize(credentials, _joinedChannel.Channel);
            
            
            
        }

        

        private void _twitchClient_OnMessageReceived(object sender, OnMessageReceivedArgs e)
        {
            switch (e.ChatMessage.Message)
            {
                case "noob":
                    SendMessage($"{e.ChatMessage.Username} No, you're a noob! Kappa");
                    break;
            }//TODO: A way to do this outside of the twitch class.
        }

        public async Task Connect()
        {
            _twitchClient.Connect();
            _twitchClient.OnConnected += TwitchClientOnConnected;

            await _connectionCompletionTask.Task;


        }

        public async Task Disconnect()
        {

            _twitchClient.Disconnect();
            _twitchClient.OnDisconnected += TwitchClientOnDisconnected;

            await _disconnectionCompletionTask.Task;

        }

        public void WireUpCommandReceivedEventHandler(Action<IChatClient, CommandReceivedEventArgs> eventHandler)
        {
            _twitchClient.OnChatCommandReceived += 
                (sender, args) => eventHandler(this, args.ToCommandReceivedEventArgs());
        }
        

        private void TwitchClientOnDisconnected(object sender, OnDisconnectedArgs e)
        {
            
            
            _twitchClient.OnDisconnected -= TwitchClientOnDisconnected;
            _isReady = false;
            _disconnectionCompletionTask.SetResult(true);
            _connectionCompletionTask = new TaskCompletionSource<bool>();
            SendMessage("Liphe is leaving for now, chill out");


        }

       

        private void TwitchClientOnConnected(object sender, OnConnectedArgs e)
        {
            _isReady = true;
            _twitchClient.OnConnected -= TwitchClientOnConnected;
            _connectionCompletionTask.SetResult(true);
            
            _disconnectionCompletionTask = new TaskCompletionSource<bool>();
            SendMessage("LipheBot has arrived!");
        }

        public void SendMessage(string message)
        {
            if(_isReady)
            {
                _twitchClient.SendMessage(_joinedChannel,message);
                
            }
           
        }

        public void WireUpChatReceivedMessageEventHandler(Action<IChatClient, ChatMessageReceivedArgs> eventHandler)
        {
            _twitchClient.OnMessageReceived +=
                (sender, args) => eventHandler(this,args.ToMessageReceivedEventArgs());
        }
    }
}
