using System.Collections.Generic;

namespace PDGames.Systems.Commands
{
    public enum CommandPriority
    {
        Low = -1, //Will be removed when any other command in queue
        Normal = 0,
        High = 1,
        Critical = 2 //Will remove all commands with lower priority when added
    }

    public sealed class CommandPriorityComparer : IEqualityComparer<CommandPriority>
    {
        public bool Equals(CommandPriority x, CommandPriority y)
        {
            return x == y;
        }

        public int GetHashCode(CommandPriority x)
        {
            return (int)x; 
        }
    }
}
