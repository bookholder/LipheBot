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
        private readonly TwitchClientSettings _settings;
        private TaskCompletionSource<bool> _connectionCompletionTask = new TaskCompletionSource<bool>();
        private TaskCompletionSource<bool> _disconnectionCompletionTask = new TaskCompletionSource<bool>();
        private bool _isReady = false;


        public TwitchChatClient(TwitchClientSettings settings)
        {
            _settings = settings;
            var credentials = new ConnectionCredentials(settings.TwitchUsername, settings.TwitchBotOAuth);
            _twitchClient = new TwitchClient();
            _twitchClient.Initialize(credentials, channel:settings.TwitchChannel);
            
            
        }

        public async Task Connect()
        {
            _twitchClient.OnConnected += TwitchClientConnected;
            _twitchClient.Connect();

            await _connectionCompletionTask.Task;
        }

        public void SendMessage(string message)
        {
             JoinedChannel jc = new JoinedChannel("channel");
            _twitchClient.SendMessage(jc,message);  
           
        }

        private void TwitchClientConnected(object sender,OnConnectedArgs onConnectedArgs)
        {
            _twitchClient.OnConnected -= TwitchClientConnected;
            _connectionCompletionTask.SetResult(true);
            _isReady = true;
            _disconnectionCompletionTask = new TaskCompletionSource<bool>();
            SendMessage("Hello, Liphe has arrived");
        }

       
            
    }
}
