using System.Collections.Generic;
using System;

namespace PDGames.EventBus
{
    [Serializable]
    public sealed class EventCollectorSerializedData
    {
        [Serializable]
        public struct EventSerializedData
        {
            public string eventName;
            public int eventCount;
        }

        public int eventsCount;
        public List<EventSerializedData> eventsData = new List<EventSerializedData>();

        public void Fill(Dictionary<Type, List<EventData>> collectedEvents)
        {
            Clear();
            foreach (var eventType in collectedEvents)
            {
                if (eventType.Value.Count > 0)
                {
                    var eventSerializedData = new EventSerializedData()
                    {
                        eventName = eventType.Key.Name,
                        eventCount = eventType.Value.Count
                    };
                    eventsData.Add(eventSerializedData);
                }
                eventsCount += eventType.Value.Count;
            }
        }

        public void Clear()
        {
            eventsCount = 0;
            eventsData.Clear();
        }
    }
}