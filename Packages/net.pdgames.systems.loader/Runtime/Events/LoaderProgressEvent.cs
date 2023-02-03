using System;
using PDGames.EventBus;

namespace PDGames.Systems.Loader
{
    [Serializable]
    public sealed class LoaderProgressEvent : EventData
    {
        public float Progress;
        public string Description;
        public int CurrentStepIndex;
        public int TotalSteps;
    }
}