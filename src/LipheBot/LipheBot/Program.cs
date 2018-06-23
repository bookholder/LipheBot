using System;
using System.Threading;
using System.Collections.Generic;
using LipheBot.Core;

namespace LipheBot
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Initializing LipheBot...");
            Console.WriteLine("To Exit, Press CTRL + L");
            var automatedMessagingSystem = new AutomatedMessagingSystem();
            var intervalTriggeredMessage = new IntervalTriggeredMessage { DelayInMinutes = 1, Message = "Hello, I am Liphe" };
            automatedMessagingSystem.Publish(intervalTriggeredMessage);

            List<IChatClient> connectedClients = new List<IChatClient> { new  ConsoleChatClient()};

            while(true)
            {
                Thread.Sleep(1000);
                automatedMessagingSystem.CheckMessages(DateTime.Now);
                while(automatedMessagingSystem.DequeueMessage(out string theMessage))
                {
                    var message = ($"{DateTime.Now.ToShortTimeString()} - {theMessage}");
                    foreach (var chatClient in connectedClients)
                    {
                        Console.WriteLine(chatClient);
                    }
                }
            }
        }
    }
}
