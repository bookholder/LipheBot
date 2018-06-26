using System;
using System.Collections.Generic;
using System.Text;

namespace LipheBot.Core
{
    public interface IChatClient
    {
        
        void SendMessage(string message);
        //void SendDirectMessage(string username,string message);

    }
}
