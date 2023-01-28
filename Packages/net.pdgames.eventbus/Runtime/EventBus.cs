using System.Collections.Generic;
using System;
using UnityEngine;

namespace PDGames.EventBus
{
    public abstract class EventBus : MonoBehaviour, IEventBus
    {
        protected Dictionary<Type, List<object>> listenersDictionary = new Dictionary<Type, List<object>>();
        [SerializeField]
        protected bool storeAllEventsInCollector = false;
        [SerializeField]
        protected EventCollector collector = new EventCollector();

        #region MONO BEHAVIOUR

        private void OnDestroy()
        {
            Deinitialize();
        }

        #endregion

        public virtual void Initialize()
        {
        }

        public virtual void Deinitialize()
        {
            foreach (var context in listenersDictionary)
            {
                context.Value.Clear();
            }

            listenersDictionary.Clear();
            collector.ClearAll();
        }

        public void Register<T1>(Action<T1> callback) where T1 : EventData
        {
            Type eventType = typeof(T1);
            if (!listenersDictionary.ContainsKey(eventType))
            {
                listenersDictionary.Add(eventType, new List<object>());
            }

            if (!listenersDictionary[eventType].Contains(callback))
            {
                listenersDictionary[eventType].Add(callback);
            }
        }

        public void Unregister<T1>(Action<T1> callback) where T1 : EventData
        {
            Type eventType = typeof(T1);
            if (listenersDictionary.ContainsKey(eventType) &&
                listenersDictionary[eventType].Contains(callback))
            {
                listenersDictionary[eventType].Remove(callback);
            }
        }

        public void Fire<T1>(T1 eventData) where T1 : EventData, new()
        {
            if (eventData == null) eventData = new T1();
            Type eventType = typeof(T1);
            if (storeAllEventsInCollector || eventData.StoreInCollector) collector.AddEvent(eventData);

            if (listenersDictionary.ContainsKey(eventType))
            {
                foreach (var listenerCallback in listenersDictionary[eventType])
                {
                    (listenerCallback as Action<T1>)(eventData);
                }
            }
        }

        public void Fire<T1>() where T1 : EventData, new()
        {
            Fire<T1>(new T1());
        }

        public EventCollector GetCollector()
        {
            return collector;
        }
    }
}