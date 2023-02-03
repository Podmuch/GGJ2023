using System;
using PDGames.EventBus;

namespace BoxColliders.Project
{
    [Serializable]
    public sealed class InitializeConfigurationEvent : EventData
    {
        public bool IsProcessed;
    }
}