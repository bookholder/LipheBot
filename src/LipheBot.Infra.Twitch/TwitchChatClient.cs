using LipheBot.Core;
using System.Configuration;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
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


        public TwitchChatClient(string channel,string username,string oauth)
        {
            
             _joinedChannel = new JoinedChannel(channel);
             var credentials = new ConnectionCredentials(username, oauth); 
            _twitchClient = new TwitchClient();
            _twitchClient.Initialize(credentials, _joinedChannel.Channel);
            _twitchClient.Connect();
            _twitchClient.OnConnected += TwitchClientOnConnected;
            
            
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
