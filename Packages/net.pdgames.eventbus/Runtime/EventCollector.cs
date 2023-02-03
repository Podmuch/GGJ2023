using System.Collections.Generic;
using UnityEngine;
using System;

namespace PDGames.EventBus
{
	[Serializable]
	public sealed class EventCollector : ISerializationCallbackReceiver
	{
		private Dictionary<Type, List<EventData>> collectedEvents = new Dictionary<Type, List<EventData>>();

        [ReadOnlyField]
        [SerializeField] 
		private EventCollectorSerializedData serializedData = new EventCollectorSerializedData();
		private bool somethingChanged = false;
		
		#region SERIALIZATION CALLBACK RECEIVER

		public void OnBeforeSerialize()
		{
			#if UNITY_EDITOR
			if (somethingChanged)
			{
				serializedData.Fill(collectedEvents);
				somethingChanged = false;
			}
			#endif
		}

		public void OnAfterDeserialize()
		{
			#if UNITY_EDITOR
			serializedData.Clear();
			somethingChanged = false;
			#endif
		}
		
		#endregion
		
		public void AddEvent(EventData eventToAdd)
		{
			Type eventType = eventToAdd.GetType();
			if (!collectedEvents.ContainsKey(eventType))
			{
				collectedEvents.Add(eventType, new List<EventData>());
			}

			collectedEvents[eventType].Add(eventToAdd);
			somethingChanged = true;
		}

		public void RemoveEvent(EventData eventToRemove)
		{
			Type eventType = eventToRemove.GetType();
			if (collectedEvents.ContainsKey(eventType))
			{
				collectedEvents[eventType].Remove(eventToRemove);
				serializedData.Clear();
				somethingChanged = true;
			}
		}

		public void ClearDestroyed()
		{
			int destroyedEvents = 0;
			foreach (var eventType in collectedEvents)
			{
				destroyedEvents += eventType.Value.RemoveAll((ev) => ev.IsDestroyed);
			}

			if (destroyedEvents > 0)
			{
				serializedData.Clear();
				somethingChanged = true;
			}
		}

		public void ClearAll()
		{
			collectedEvents.Clear();
			serializedData.Clear();
			somethingChanged = true;
		}

		public List<EventData> GetEventsWithType<T1>() where T1 : EventData
		{
			Type eventType = typeof(T1);
			if (collectedEvents.ContainsKey(eventType))
			{
				return collectedEvents[eventType];
			}

			return new List<EventData>();
		}

		public T1 GetFirstEventWithType<T1>() where T1 : EventData
		{
			Type eventType = typeof(T1);
			if (collectedEvents.ContainsKey(eventType))
			{
				var events = collectedEvents[eventType];
				if (events.Count > 0) return events[0] as T1;
			}
			return default;
		}
	}
}