using System;
using PDGames.EventBus;

namespace BoxColliders.Project
{
    [Serializable]
    public sealed class SetBranchIndicatorEvent : EventData
    {
        public int index;
    }
}