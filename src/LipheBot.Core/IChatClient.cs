

using System;

namespace LipheBot.Core
{
    public interface IChatClient
    {
        
        void SendMessage(string message);

        void Connect();  //TODO: Consider changing this to a task

        //void SendDirectMessage(string username,string message);
        void Disconnect();

        void WireUpCommandReceivedEventHandler(Action<IChatClient,CommandReceivedEventArgs> eventHandler);


    }
}
