using System.Collections.Generic;
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
        public BotMain()
        {
            _chatClients = new List<IChatClient>();
        }

        public async Task Run()
        {
           
            await ConnectChatClients();
            WireUpEventHandlers();
            
        }

        public async Task Stop()
        {
            await DisconnectChatClients();
        }

        private async Task ConnectChatClients()
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
                chatclient.WireUpCommandReceivedEventHandler(CommandReceivedHandler);
                chatclient.WireUpChatReceivedMessageEventHandler(ChatMessageReceivedHandler);
            }
        }

        

        private void CommandReceivedHandler(IChatClient chatClient, CommandReceivedEventArgs args)
        {
            
            switch (args.CommandWord)
            {
                case "liphe":
                    chatClient.SendMessage($"@{args.Username}, I'm still in development");
                break;
            }
        }
        private void ChatMessageReceivedHandler(IChatClient chatClient,ChatMessageReceivedArgs args)
        {
            switch (args.Message.ToLower())
            {
                case "noob":
                    chatClient.SendMessage($"@{args.Username}, Im not too good at this game");
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
