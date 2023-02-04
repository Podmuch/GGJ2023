using System.Collections.Generic;
using PDGames.DIContainer;
using PDGames.EventBus;
using PDGames.Systems;
using PDGames.Systems.Loader;

namespace BoxColliders.Project
{
    public sealed class TransitionInitialLoadingSystem : ReactiveSystem<TransitionStartInitialLoadingEvent>, IInitializeSystem
    {
        [DIInject] 
        private LoaderQueue loaderQueue = default;
        
        private IDIContainer diContainer;
        
        public TransitionInitialLoadingSystem(IEventBus eventBus, IDIContainer diContainer) : base(eventBus)
        {
            this.diContainer = diContainer;
        }

        public void Initialize()
        {
            diContainer.Fetch(this);
            eventBus.Fire<TransitionStartInitialLoadingEvent>();
        }

        protected override void Execute(List<TransitionStartInitialLoadingEvent> events)
        {
            for (int i = 0; i < events.Count; i++)
            {
                events[i].IsDestroyed = true;
            }
            
            StartInitialLoading();
        }

        private void StartInitialLoading()
        {
            loaderQueue.Steps.Add(new TransitionLoadMainSceneStep(eventBus, diContainer));
            loaderQueue.Steps.Add(new TransitionInitializeMonoBehaviourHostStep(eventBus, diContainer));
            loaderQueue.Steps.Add(new TransitionInitializeEventSystemStep(eventBus, diContainer));
            loaderQueue.Steps.Add(new TransitionLoadConfigurationStep(eventBus, diContainer));
            loaderQueue.Steps.Add(new TransitionInitializeMainCameraStep(eventBus, diContainer));
            loaderQueue.Steps.Add(new TransitionInitializeAudioControllerStep(eventBus, diContainer));
            loaderQueue.Steps.Add(new TransitionLoadInitialBackgroundStep(eventBus, diContainer));
            loaderQueue.Steps.Add(new TransitionGenerateRandomTreeStep(eventBus, diContainer));
            loaderQueue.Steps.Add(new TransitionInitialLoadingEndStep(eventBus, diContainer));
            
            loaderQueue.IsStarted = true;
        }
    }
}