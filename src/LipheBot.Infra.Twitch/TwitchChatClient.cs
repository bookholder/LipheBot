using System;
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
        
        //private TaskCompletionSource<bool> _connectionCompletionTask = new TaskCompletionSource<bool>();
        //private TaskCompletionSource<bool> _disconnectionCompletionTask = new TaskCompletionSource<bool>();
        private bool _isReady;


        public TwitchChatClient(TwitchClientSettings settings)
        {
            
             _joinedChannel = new JoinedChannel(settings.TwitchChannel);
             var credentials = new ConnectionCredentials(settings.TwitchUsername, settings.TwitchBotOAuth); 
            _twitchClient = new TwitchClient();
            _twitchClient.Initialize(credentials, _joinedChannel.Channel);
            
            _twitchClient.OnNewSubscriber += TwitchClientOnNewSubscriber;
            _twitchClient.OnChatCommandReceived += TwitchClientOnChatCommandReceived;

        }

        private void TwitchClientOnChatCommandReceived(object sender, OnChatCommandReceivedArgs onCommandEventArgs)
        {
            switch (onCommandEventArgs.Command.CommandText)
            {
                case "noob":
                    SendMessage("No, You're a noob!");
                    break;
            }
        }

        public void Connect()
        {
            _twitchClient.Connect();
            _twitchClient.OnConnected += TwitchClientOnConnected;
            
            
      
        }

        public void Disconnect()
        {
            _twitchClient.Disconnect();
            _twitchClient.OnDisconnected += TwitchClientOnDisconnected;
        }

        public void WireUpCommandReceivedEventHandler(Action<IChatClient, CommandReceivedEventArgs> eventHandler)
        {
            _twitchClient.OnChatCommandReceived += (sender, args) => eventHandler(this, new CommandReceivedEventArgs());
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
