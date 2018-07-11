

using System;
using System.Threading.Tasks;

namespace LipheBot.Core
{
    public interface IChatClient
    {
        
        void SendMessage(string message);

        Task Connect();  

        //void SendDirectMessage(string username,string message);
        Task Disconnect();


        void WireUpCommandReceivedUpEventHandler(Action<IChatClient,CommandReceivedEventArgs> eventHandler);

    }
}
