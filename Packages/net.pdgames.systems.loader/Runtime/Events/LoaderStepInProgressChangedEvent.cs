using System;
using PDGames.EventBus;

namespace PDGames.Systems.Loader
{
    [Serializable]
    public sealed class LoaderStepInProgressChangedEvent : EventData
    {
        public LoaderStep Step;
    }
}