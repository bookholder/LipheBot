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
        //private readonly TwitchClientSettings _settings;
        private  readonly JoinedChannel _joinedChannel;
        
        private readonly TaskCompletionSource<bool> _connectionCompletionTask = new TaskCompletionSource<bool>();
        private readonly TaskCompletionSource<bool> _disconnectionCompletionTask = new TaskCompletionSource<bool>();
        private bool _isReady;

        

        public TwitchChatClient(TwitchClientSettings settings)
        {
            
             _joinedChannel = new JoinedChannel(settings.TwitchChannel);
             var credentials = new ConnectionCredentials(settings.TwitchUsername, settings.TwitchBotOAuth); 
            _twitchClient = new TwitchClient();
            _twitchClient.Initialize(credentials, _joinedChannel.Channel);
            _twitchClient.OnMessageReceived += _twitchClient_OnMessageReceived;
            _twitchClient.OnChatCommandReceived += _twitchClient_OnChatCommandReceived;
        }

        private void _twitchClient_OnChatCommandReceived(object sender, OnChatCommandReceivedArgs e)
        {
            
        }

        private void _twitchClient_OnMessageReceived(object sender, OnMessageReceivedArgs e)
        {
            switch (e.ChatMessage.Message)
            {
                case "noob":
                    SendMessage($"{e.ChatMessage.Username} No, You're a noob!");
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

        public void WireUpCommandReceivedUpEventHandler(Action<IChatClient, CommandReceivedEventArgs> eventHandler)
        {
            _twitchClient.OnChatCommandReceived += 
                (sender, args) => eventHandler(this, args.ToCommandReceivedEventArgs());
        }
        

        private void TwitchClientOnDisconnected(object sender, OnDisconnectedArgs e)
        {
            
            SendMessage("Liphe is leaving for now, chill out");
            
        }

       

        private void TwitchClientOnConnected(object sender, OnConnectedArgs e)
        {
            _isReady = true;
            _twitchClient.OnConnected -= TwitchClientOnConnected;
            SendMessage("LipheBot has arrived!");
        }

        public void SendMessage(string message)
        {
            if(_isReady)
            {
                _twitchClient.SendMessage(_joinedChannel,message);
                
            }
           
        }


      







    }
}
