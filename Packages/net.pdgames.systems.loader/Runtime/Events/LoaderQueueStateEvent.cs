using System;
using PDGames.EventBus;

namespace PDGames.Systems.Loader
{
    [Serializable]
    public sealed class LoaderQueueStateEvent : EventData
    {
        public bool ShouldStart;
    }
}