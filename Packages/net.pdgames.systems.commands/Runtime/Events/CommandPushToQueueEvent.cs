using System;
using PDGames.EventBus;

namespace PDGames.Systems.Commands
{
    [Serializable]
    public sealed class CommandPushToQueueEvent : EventData
    {
        public CommandPriority Priority;
        public AbstractCommand Command;
    }
}
