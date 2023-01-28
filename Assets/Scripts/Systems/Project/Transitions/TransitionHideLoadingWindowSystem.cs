using System.Collections.Generic;
using PDGames.DIContainer;
using PDGames.EventBus;
using PDGames.Systems;
using PDGames.Systems.Loader;
using PDGames.UserInterface;
using BoxColliders.Windows;

namespace BoxColliders.Project
{
    public sealed class TransitionHideLoadingWindowSystem : ReactiveSystem<LoaderFinishedEvent>, IInitializeSystem
    {
        private IDIContainer diContainer;
        
        public TransitionHideLoadingWindowSystem(IEventBus eventBus, IDIContainer diContainer) : base(eventBus)
        {
            this.diContainer = diContainer;
        }

        public void Initialize()
        {
            diContainer.Fetch(this);
        }
        
        protected override void Execute(List<LoaderFinishedEvent> entities)
        {
            eventBus.Fire(new UiHideWindowEvent(){ Type = typeof(LoadingWindow) });
        }
    }
}