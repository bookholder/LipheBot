using System;
using System.Threading.Tasks;
using LipheBot.Core;

namespace LipheBot
{
    public class ConsoleChatClient : IChatClient
    {

        private TaskCompletionSource<bool> _connectionCompletionTask = new TaskCompletionSource<bool>();
        private TaskCompletionSource<bool> _disconnectionCompletionTask = new TaskCompletionSource<bool>();
        public void SendMessage(string message) => Console.WriteLine("Bot: " + message);

        public async Task Connect()
        {
            SendMessage("LipheBot has arrived");
            _connectionCompletionTask.SetResult(true);
            await _connectionCompletionTask.Task;
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
