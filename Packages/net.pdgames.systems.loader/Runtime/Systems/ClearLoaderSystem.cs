using System.Collections.Generic;
using PDGames.DIContainer;
using PDGames.EventBus;

namespace PDGames.Systems.Loader
{
    public sealed class ClearLoaderSystem : ReactiveSystem<LoaderClearEvent>, IInitializeSystem
    {
        [DIInject] private LoaderQueue loaderQueue = default;

        private IDIContainer diContainer;

        public ClearLoaderSystem(IEventBus eventBus, IDIContainer diContainer) : base(eventBus)
        {
            this.diContainer = diContainer;
        }

        public void Initialize()
        {
            diContainer.Fetch(this);
        }

        protected override void Execute(List<LoaderClearEvent> events)
        {
            for (int i = 0; i < events.Count; i++)
            {
                events[i].IsDestroyed = true;
            }

            loaderQueue.Steps.Clear();
            loaderQueue.StepInProgress = null;
        }
    }
}