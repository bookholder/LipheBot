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
            };
            return commandReceivedEventArgs;
        }
    }
}