using System;
using LipheBot.Core;
using TwitchLib;
using TwitchLib.Client.Models;
using TwitchLib.Client;


namespace LipheBot.Infra.Twitch
{
    public class TwitchChatClient : IChatClient
    {
        private readonly TwitchClient _twitchClient;
        public TwitchChatClient()
        {
            var connecectionCredentials = new ConnectionCredentials("username", "OAuth");
            _twitchClient.Initialize(connecectionCredentials, "channel");
            
        }

        public void SendMessage(string message)
        {
             JoinedChannel jc = new JoinedChannel("channel");
            _twitchClient.SendMessage(jc,message);  
           
        }
            
    }
}
