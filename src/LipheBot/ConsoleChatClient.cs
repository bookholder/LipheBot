using System;
using System.Threading.Tasks;
using LipheBot.Core;

namespace LipheBot
{
    public class ConsoleChatClient : IChatClient
    {
        public void SendMessage(string message) => Console.WriteLine(message);

        public async Task Connect()
        {
            //
        }

        public async Task Disconnect()
        {
            //
        }

        public void WireUpCommandReceivedUpEventHandler(Action<IChatClient, CommandReceivedEventArgs> eventHandler)
        {
            //
        }

        public event EventHandler<CommandReceivedEventArgs> OnCommandReceived;

        public void WireUpCommandReceivedEventHandler(Action<IChatClient, CommandReceivedEventArgs> eventHandler)
        {
            
        }

        public void WireUpChatReceivedMessageEventHandler(Action<IChatClient, ChatMessageReceivedArgs> eventHandler)
        {
            //
        }
    }
}
