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
           
            ConnectChantClients();
            WireUpEventHandlers();
            
        }

        private void ConnectChantClients()
        {
            foreach (var chatclient in _chatClients)
            {
                chatclient.Connect();
            }
        }

        private void WireUpEventHandlers()
        {
            foreach (var chatclient in _chatClients)
            {
                chatclient.WireUpCommandReceivedUpEventHandler(CommandReceivedHandler);
            }
        }

        

        private void CommandReceivedHandler(IChatClient chatClient, CommandReceivedEventArgs args)
        {
            
            switch (args.CommandWord)
            {
                case "liphe":
                    chatClient.SendMessage($"@{args.Username}, I'm still in devolpment");
                break;
            }
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
