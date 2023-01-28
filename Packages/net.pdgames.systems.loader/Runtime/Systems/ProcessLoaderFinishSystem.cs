using System.Collections.Generic;
using PDGames.DIContainer;
using PDGames.EventBus;

namespace PDGames.Systems.Loader
{
    public sealed class ProcessLoaderFinishSystem : ReactiveSystem<LoaderFinishedEvent>, IInitializeSystem
    {
        [DIInject] 
        private LoaderQueue loaderQueue = default;
        
        private IDIContainer diContainer;
        
        public ProcessLoaderFinishSystem(IEventBus eventBus, IDIContainer diContainer) : base(eventBus)
        {
            this.diContainer = diContainer;
        }

        public void Initialize()
        {
            diContainer.Fetch(this);
        }
        
        protected override void Execute(List<LoaderFinishedEvent> events)
        {
            for (int i = 0; i < events.Count; i++)
            {
                events[i].IsDestroyed = true;
            }

            if (loaderQueue.IsStarted)
            {
                loaderQueue.IsStarted = false;
                eventBus.Fire<LoaderClearEvent>(new LoaderClearEvent());
            }
        }
    }
}