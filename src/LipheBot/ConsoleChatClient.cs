using System;

using LipheBot.Core;

namespace LipheBot
{
    public class ConsoleChatClient : IChatClient
    {
        public void SendMessage(string message) => Console.WriteLine(message);

        public void Connect()
        {

        }

        public void Disconnect()
        {
            throw new NotImplementedException();
        }

        public void WireUpCommandReceivedEventHandler(Action<IChatClient, CommandReceivedEventArgs> eventHandler)
        {
            throw new NotImplementedException();
        }
    }
}
