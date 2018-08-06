using LipheBot.Core;
using TwitchLib.Client.Events;

namespace LipheBot.Infra.Twitch
{
    public static class EventArgsExtensions
    {
        public static CommandReceivedEventArgs ToCommandReceivedEventArgs(this OnChatCommandReceivedArgs src)
        {
            var commandReceivedEventArgs = new CommandReceivedEventArgs
            {
                CommandWord = src.Command.CommandText,
                Username = src.Command.ChatMessage.Username,
            };
            return commandReceivedEventArgs;
        }

        public static ChatMessageReceivedArgs ToMessageReceivedEventArgs(this OnMessageReceivedArgs src)
        {
            var chatMessageReceivedEventArgs = new ChatMessageReceivedArgs
            {
                Message = src.ChatMessage.Message,
                Username = src.ChatMessage.Username,
               
            };

            return chatMessageReceivedEventArgs;
        }
    }
}