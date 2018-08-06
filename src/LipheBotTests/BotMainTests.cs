using System;
using Xunit;
using LipheBot.Core;
using System.Threading.Tasks;

namespace LipheBotTests
{
    
    public  class BotMainTests
    {
        [Fact]
        public async Task Intialization()
        {
            var botMain = new BotMain();

            await botMain.Run();

           
        }

        
    }
}
