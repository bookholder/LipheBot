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
            
            
            
        }

        public void Connect()
        {
            _twitchClient.Connect();
            _twitchClient.OnConnected += TwitchClientOnConnected;
            _twitchClient.OnNewSubscriber += _twitchClient_OnNewSubscriber;
            

        }

        private void _twitchClient_OnNewSubscriber(object sender, OnNewSubscriberArgs e)
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
