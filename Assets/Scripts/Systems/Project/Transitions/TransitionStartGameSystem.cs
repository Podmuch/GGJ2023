using System.Collections.Generic;
using PDGames.DIContainer;
using PDGames.EventBus;
using PDGames.Systems;
using PDGames.Systems.Loader;

namespace BoxColliders.Project
{
    public sealed class TransitionStartGameSystem : ReactiveSystem<UiStartGameRequestEvent>, IInitializeSystem
    {
        [DIInject] 
        private LoaderQueue loaderQueue = default;
        
        private IDIContainer diContainer;
        
        public TransitionStartGameSystem(IEventBus eventBus, IDIContainer diContainer) : base(eventBus)
        {
            this.diContainer = diContainer;
        }

        public void Initialize()
        {
            diContainer.Fetch(this);
        }
        
        protected override void Execute(List<UiStartGameRequestEvent> events)
        {
            for (int i = 0; i < events.Count; i++)
            {
                events[i].IsDestroyed = true;
            }
            
            StartGameLoading();
        }

        private void StartGameLoading()
        {
            loaderQueue.Steps.Add(new TransitionGameLoadingStartStep(eventBus, diContainer));
            loaderQueue.Steps.Add(new TransitionCreateGameSystemsCascadeStep(eventBus, diContainer));
            loaderQueue.Steps.Add(new TransitionCreateObjectsPoolStep(eventBus, diContainer));
            loaderQueue.Steps.Add(new TransitionLoadGameControllersStep(eventBus, diContainer));
            loaderQueue.Steps.Add(new TransitionInitializeGameStep(eventBus, diContainer));
            loaderQueue.Steps.Add(new TransitionGameLoadingEndStep(eventBus, diContainer));
            loaderQueue.IsStarted = true;
        }
    }
}