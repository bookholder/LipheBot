using System;
using System.Collections.Generic;
using System.Text;
using LipheBot.Core.Data.Model;

namespace LipheBot.Core.Commands
{
    public interface IBotCommand
    {
        UserRole RoleRequired { get; }
        TimeSpan Cooldown { get; }
        string PrimaryCommandText { get; }
        string HelpText { get; }
        string FullHelpText { get; }
        bool ShouldExecute(string commandText);

        
    }
}
