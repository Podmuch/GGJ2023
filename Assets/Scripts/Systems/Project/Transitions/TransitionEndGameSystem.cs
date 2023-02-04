using System.Collections.Generic;
using PDGames.DIContainer;
using PDGames.EventBus;
using PDGames.Systems;
using PDGames.Systems.Loader;

namespace BoxColliders.Project
{
    public sealed class TransitionEndGameSystem : ReactiveSystem<UiEndGameRequestEvent>, IInitializeSystem
    {
        [DIInject] 
        private LoaderQueue loaderQueue = default;
        
        private IDIContainer diContainer;
        
        public TransitionEndGameSystem(IEventBus eventBus, IDIContainer diContainer) : base(eventBus)
        {
            this.diContainer = diContainer;
        }

        public void Initialize()
        {
            diContainer.Fetch(this);
        }
        
        protected override void Execute(List<UiEndGameRequestEvent> events)
        {
            for (int i = 0; i < events.Count; i++)
            {
                events[i].IsDestroyed = true;
            }
            
            EndGameLoading();
        }

        private void EndGameLoading()
        {
            loaderQueue.Steps.Add(new TransitionMenuLoadingStartStep(eventBus, diContainer));
            loaderQueue.Steps.Add(new TransitionClearGameplaySystemsStep(eventBus, diContainer));
            loaderQueue.Steps.Add(new TransitionClearResourcesStep(eventBus, diContainer));
            loaderQueue.Steps.Add(new TransitionGenerateRandomTreeStep(eventBus, diContainer));
            loaderQueue.Steps.Add(new TransitionMenuLoadingEndStep(eventBus, diContainer));
            loaderQueue.IsStarted = true;
        }
    }
}