using System;

namespace PDGames.EventBus
{
    [Serializable]
    public class EventData
    {
        public bool StoreInCollector = false;
        public bool IsDestroyed = false;
    }
}