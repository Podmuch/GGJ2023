using System.Collections.Generic;
using PDGames.EventBus;

namespace PDGames.Systems
{
    public abstract class ReactiveSystem<T> : IExecuteSystem where T : EventData
    {
        protected IEventBus eventBus;
        private List<T> firedEventsList = new List<T>();
        private List<T> cachedEventsList = new List<T>();

        private bool isProcessing = false;

        public ReactiveSystem(IEventBus eventBus)
        {
            this.eventBus = eventBus;
            eventBus.Register<T>(GatherEvent);
        }

        ~ReactiveSystem()
        {
            eventBus.Unregister<T>(GatherEvent);
        }

        public void Execute()
        {
            if (firedEventsList.Count > 0)
            {
                isProcessing = true;
                Execute(firedEventsList);
                
                firedEventsList.Clear();
                if (cachedEventsList.Count > 0)
                {
                    firedEventsList.AddRange(cachedEventsList);
                    cachedEventsList.Clear();
                }
                isProcessing = false;
            }
        }

        protected abstract void Execute(List<T> events);

        private void GatherEvent(T eventCall)
        {
            if (!isProcessing)
            {
                firedEventsList.Add(eventCall);
            }
            else
            {
                cachedEventsList.Add(eventCall);
            }
        }
    }
}