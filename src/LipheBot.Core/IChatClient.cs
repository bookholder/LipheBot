

namespace LipheBot.Core
{
    public interface IChatClient
    {
        
        void SendMessage(string message);

        void Connect();
        //void SendDirectMessage(string username,string message);

    }
}
