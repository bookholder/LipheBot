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
        private readonly JoinedChannel _joinedChannel;
        
        //private TaskCompletionSource<bool> _connectionCompletionTask = new TaskCompletionSource<bool>();
        //private TaskCompletionSource<bool> _disconnectionCompletionTask = new TaskCompletionSource<bool>();
        private bool _isReady;


        public TwitchChatClient()
        {

             _joinedChannel = new JoinedChannel("Pr0blems_");
             var credentials = new ConnectionCredentials("LipheBot", "l4d6xao3m9yfj768qd2kf0tdapqtg1"); 
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
