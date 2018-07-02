using System;
using System.Collections.Generic;
using System.Text;
using LipheBot.Core;

namespace LipheBot
{
    public class ConsoleChatClient : IChatClient
    {
        public void SendMessage(string message)
        {
            Console.WriteLine(message);
        }

        public void Connect()
        {

        }
    }
}
