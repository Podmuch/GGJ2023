using System;

namespace PDGames.EventBus
{
    public interface IEventBus
    {
        void Initialize();
        void Register<T1>(Action<T1> callback) where T1 : EventData;
        void Unregister<T1>(Action<T1> callback) where T1 : EventData;
        void Fire<T1>(T1 eventObj) where T1 : EventData, new();
        void Fire<T1>() where T1 : EventData, new();
    }
}