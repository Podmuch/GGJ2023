using UnityEngine;

namespace PDGames.EventBus
{
    public abstract class EventBusSingleton<TBus> : EventBus where TBus : MonoBehaviour
    {
        protected static IEventBus instance;

        public static IEventBus Instance
        {
            get
            {
                if (instance == null)
                {
                    var ins = FindObjectOfType<TBus>();
                    if (ins != null) instance = ins as IEventBus;
                }

                return instance;
            }
            protected set { instance = value; }
        }

        public override void Initialize()
        {
            base.Initialize();
            if (instance == null) Instance = this;
        }

        public override void Deinitialize()
        {
            base.Deinitialize();
            if (ReferenceEquals(instance, this)) Instance = null;
        }
    }
}