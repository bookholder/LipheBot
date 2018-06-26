using System;
using System.Threading;
using System.Collections.Generic;
using LipheBot.Core;
using LipheBot.Infra.Twitch;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace LipheBot
{
    class Program
    {
        public static IConfiguration Configuration { get; set; }
        static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json");

            Configuration = builder.Build();

            var configurationSection = Configuration.GetSection("TwitchChatClient");

            Console.WriteLine("Initializing LipheBot...");
            Console.WriteLine("To Exit, Press CTRL + L");
            var automatedMessagingSystem = new AutomatedMessagingSystem();
            var intervalTriggeredMessage = new IntervalTriggeredMessage { DelayInMinutes = 1, Message = "Hello, I am Liphe the bot" };
            automatedMessagingSystem.Publish(intervalTriggeredMessage);
            List<IChatClient> connectedClients = ConnectChatClients();

            while (true)
            {
                Thread.Sleep(1000);
                automatedMessagingSystem.CheckMessages(DateTime.Now);
                while (automatedMessagingSystem.DequeueMessage(out string theMessage))
                {
                    var message = ($"{DateTime.Now.ToShortTimeString()} - {theMessage}");
                    foreach (IChatClient item in connectedClients)
                    {
                        item.SendMessage(message);
                    }


                }
            }
        }

        private static List<IChatClient> ConnectChatClients()
        {
            var connectChatClients = new List<IChatClient>
            {
                new ConsoleChatClient(),
                new TwitchChatClient(),
            };
            Thread.Sleep(1000);
            return connectChatClients;
           
        }
        
    }
}
