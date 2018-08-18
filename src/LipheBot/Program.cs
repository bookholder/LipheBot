using LipheBot.Core;
using LipheBot.Infra.Twitch;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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
            
            Console.WriteLine("Available bot commands : start, stop, exit");
            Console.WriteLine("================================================");
            

            InitializeConfiguration();
            InitializeBot().Wait();

           
            




            while (true)

            {
                ConsoleCommands(Console.ReadLine());
            }


        }





        private static async Task InitializeBot()
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

            await _lipheBot.Run();
            
            Console.WriteLine("Bot initialized");
            


        }
        
        private static async Task DisconnectBot()
        {
            Console.WriteLine("Disconnecting bot...");
            await _lipheBot.Stop();
            Console.WriteLine("Disconnected");
        }

        private static void InitializeConfiguration()
        {
            Console.WriteLine("Initializing configuration..");
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            Configuration = builder.Build();
        }


    private static void ConsoleCommands(string s)
        {
            List<string> commands = new List<string>
            {
                "start",
                "stop",
                "exit"
            };
            switch (s)
            {
                case "exit":
                        
                    Process.GetCurrentProcess().Kill();
                    break;
                case "start":
                InitializeBot().Wait();
                break;

                case "stop":
                    DisconnectBot().Wait();
                break;

                case "?":
                    foreach (string command in commands)
                    {
                        Console.WriteLine(command);
                    }
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
