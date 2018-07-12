using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
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

        public async Task Run()
        {
           
            await ConnectChantClients();
            WireUpEventHandlers();
            
        }

        public async Task Stop()
        {
            await DisconnectChatClients();
        }

        private async Task ConnectChantClients()
        {
            

            var connectedTasks = new List<Task>();
            foreach (var chatClient in _chatClients)
            {
                connectedTasks.Add(chatClient.Connect());
            }
            await Task.WhenAll(connectedTasks);
            
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

        private async Task DisconnectChatClients()
        {
            foreach (var chatclient in _chatClients)
            {
                chatclient.SendMessage("Liphe is leaving for now, chill out!");
            }

            var disconnectedTasks = new List<Task>();
            foreach (var chatclient in _chatClients)
            {
                disconnectedTasks.Add(chatclient.Disconnect());
            }

            await Task.WhenAll(disconnectedTasks);
        }

    }
}
