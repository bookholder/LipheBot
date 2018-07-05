using System;
using System.Collections.Generic;
using System.Text;
using LipheBot.Core.Automation;

namespace LipheBot.Core
{
    public class BotMain
    {
        private readonly List<IChatClient> _chatClients;
        private readonly AutomatedMessagingSystem _autoSystem = new AutomatedMessagingSystem();

        public BotMain(List<IChatClient> chatClients)
        {
            _chatClients = chatClients;
        }

        public void Run()
        {
            WireUpEventHandlers();
            foreach (var chatclient in _chatClients)
            {
                chatclient.Connect();
                
            }
        }

        private void WireUpEventHandlers()
        {
            throw new NotImplementedException();
        }

        public void DisconnectChatClients()
        {
            foreach (var chatclient in _chatClients)
            {
               chatclient.Disconnect();
            }
        }

    }
}
