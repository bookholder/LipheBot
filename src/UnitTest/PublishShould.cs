
using LipheBot.Core.Automation;
using Xunit;

namespace UnitTest
{
    public class PublishShould
    {
        [Fact]
        
        public void AddAutomatedMessageToManageMessages()
        {
            var messagingSystem = new AutomatedMessagingSystem();

            var automatedMessage = new IntervalTriggeredMessage
            {

                DelayInMinutes = 1,
                Message = "Hello everyone, If you enjoy the content please consider following!"
            };
            messagingSystem.Publish(automatedMessage);

            Assert.Contains(automatedMessage,messagingSystem.ManagedMessages);
           
        }
    }
}
