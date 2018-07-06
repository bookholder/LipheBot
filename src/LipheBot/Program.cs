using LipheBot.Core;
using LipheBot.Infra.Twitch;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace LipheBot
{

    class Program
    {
        private static BotMain _lipheBot;

        public static IConfiguration Configuration { get; set; }


        private static void Main()
        {
            InitializeConfiguration();
            InitializeBot();
            
            Console.Write("Press ESC key to quit");
            
            while (true)

            {
                ConsoleCommands(Console.ReadKey().Key);
            }


        }





        private static void InitializeBot()
        {
            Console.WriteLine("Initializing LipheBot...");
            TwitchClientSettings settings = new TwitchClientSettings(
                $"{Configuration["TwitchChatClient:twitchUsername"]}",
                $"{Configuration["TwitchChatClient:twitchOAuth"]}",
                $"{Configuration["TwitchChatClient:twitchChannel"]}");
            List<IChatClient> chatClients = new List<IChatClient>

            {
                new ConsoleChatClient(),
                new TwitchChatClient(settings),
            };
            _lipheBot = new BotMain(chatClients);

            _lipheBot.Run();

        }

        private static void InitializeConfiguration()
        {
            Console.WriteLine("Initializing configuration..");
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            Configuration = builder.Build();
        }


    private static void ConsoleCommands(ConsoleKey key)
        {
            switch (key)
            {
                case ConsoleKey.Escape:
                        
                    Process.GetCurrentProcess().Kill();
                    break;
                
            }

            

        }




        }


            //var automatedMessagingSystem = new AutomatedMessagingSystem();
            //var intervalTriggeredMessage = new IntervalTriggeredMessage { DelayInMinutes = 1, Message = "Hello, I am Liphe the bot" };
            //automatedMessagingSystem.Publish(intervalTriggeredMessage);
            //while (true)
            //{
            //    Thread.Sleep(1000);
            //    automatedMessagingSystem.CheckMessages(DateTime.Now);
            //    while (automatedMessagingSystem.DequeueMessage(out string theMessage))
            //    {
            //        var message = ($"{DateTime.Now.ToShortTimeString()} - {theMessage}");



            //    }
            //}

        
    
}
