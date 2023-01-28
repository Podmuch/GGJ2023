using System;
using PDGames.EventBus;

namespace PDGames.Systems.Commands
{
    [Serializable]
    public sealed class CommandQueueChangeStateEvent : EventData
    {
        public bool ShouldOpen;
    }
}
