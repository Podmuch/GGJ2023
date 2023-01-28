using System;
using PDGames.EventBus;

namespace PDGames.Systems.Commands
{
    [Serializable]
    public sealed class CommandInProgressChangedEvent : EventData
    {
        public AbstractCommand Command;
    }
}
