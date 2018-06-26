using LipheBot.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwitchLib;
using TwitchLib.Client;
using TwitchLib.Client.Models;
using TwitchLib.Client.Events;
using TwitchLib.Client.Extensions;
using TwitchLib.Client.Interfaces;




namespace LipheBot.Infra.Twitch
{
    public class TwitchChatClient : IChatClient
    {
        private readonly ITwitchClient _twitchClient;
        //private readonly TwitchClientSettings _settings;
        private readonly JoinedChannel _joinedChannel;
        private readonly ConnectionCredentials credentials;
        //private TaskCompletionSource<bool> _connectionCompletionTask = new TaskCompletionSource<bool>();
        //private TaskCompletionSource<bool> _disconnectionCompletionTask = new TaskCompletionSource<bool>();
        private bool _isReady = false;


        public TwitchChatClient()
        {

            // _joinedChannel = new JoinedChannel();TODO: Lock Down Acces so i dont need to retype it
            //credentials = new ConnectionCredentials(); TODO: Lock down access to this field
            _twitchClient = new TwitchClient();
            _twitchClient.Initialize(credentials, _joinedChannel.Channel);
            _twitchClient.Connect();
            _twitchClient.OnConnected += TwitchClientOnConnected;
            
            
        }

        private void TwitchClientOnConnected(object sender, OnConnectedArgs e)
        {
            _isReady = true;
            SendMessage("LipheBot has Arrived!");
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
