using System.Collections.Generic;
using PDGames.DIContainer;
using PDGames.EventBus;
using PDGames.Systems;
using PDGames.Systems.Loader;
using PDGames.UserInterface;
using BoxColliders.Windows;

namespace BoxColliders.Project
{
    public sealed class TransitionShowLoadingWindowSystem : ReactiveSystem<LoaderQueueStateEvent>, IInitializeSystem
    {
        [DIInject] 
        private LoaderQueue loaderQueue = default;

        private IDIContainer diContainer;

        public TransitionShowLoadingWindowSystem(IEventBus eventBus, IDIContainer diContainer) : base(eventBus)
        {
            this.diContainer = diContainer;
        }

        public void Initialize()
        {
            diContainer.Fetch(this);
        }

        protected override void Execute(List<LoaderQueueStateEvent> entities)
        {
            if (loaderQueue.IsStarted)
            {
                eventBus.Fire(new UiShowWindowEvent(){ Type = typeof(LoadingWindow) });
            }
        }
    }
}