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
            _twitchClient.OnChatCommandReceived += ChatCommandReceived;
            _twitchClient.OnNewSubscriber += TwitchClientOnNewSubscriber;
            
            

        }

        //private void _twitchClient_OnChatCommandReceived(object sender, OnChatCommandReceivedArgs onChatCommand)
        //{
        //    switch (onChatCommand.Command.CommandText)
        //    {
        //        case "noob":
        //            SendMessage("No, you're a noob!");
        //            break;
                
        //    }
        //}

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

        private void ChatCommandReceived(object sender, OnChatCommandReceivedArgs e)
        {
            OnCommandReceived?.Invoke(this,e.ToCommandReceivedEventArgs());
        }

        


        private void TwitchClientOnDisconnected(object sender, OnDisconnectedArgs e)
        {
            
            SendMessage("Liphe is leaving for now, chill out");
            
        }

        private void TwitchClientOnNewSubscriber(object sender, OnNewSubscriberArgs e)
        {
            throw new System.NotImplementedException();
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



        public event EventHandler<CommandReceivedEventArgs> OnCommandReceived;



    }
}
