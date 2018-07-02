using LipheBot.Core;
using LipheBot.Core.Automation;
using LipheBot.Infra.Twitch;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace LipheBot
{

    class Program
    {
        public static IConfiguration Configuration { get; set; }
        

        private static void Main()
        {
            var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json");

            Configuration = builder.Build();

            
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
                    foreach (var item in connectedClients)
                    {
                        item.SendMessage(message);
                    }

                   
                }
            }
            
        }

        private static List<IChatClient> ConnectChatClients()
        {
            TwitchClientSettings settings = new TwitchClientSettings($"{Configuration["TwitchChatClient:twitchUsername"]}", $"{Configuration["TwitchChatClient:twitchOAuth"]}", $"{Configuration["TwitchChatClient:twitchChannel"]}");
            var connectChatClients = new List<IChatClient>
            {
                new ConsoleChatClient(),
                new TwitchChatClient(settings),
            };
           
            foreach (var clients in connectChatClients)
            {
                clients.Connect();
            }
            
            Thread.Sleep(1000);
            return connectChatClients;
           
            
        }


        
    }
}
