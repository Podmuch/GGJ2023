using System.Collections.Generic;
using PDGames.DIContainer;
using PDGames.EventBus;

namespace PDGames.Systems.Loader
{
    public sealed class ProcessLoaderStepFinishedSystem : ReactiveSystem<LoaderStepFinishedEvent>, IInitializeSystem
    {
        [DIInject] 
        private LoaderQueue loaderQueue = default;
        
        private IDIContainer diContainer;
        
        public ProcessLoaderStepFinishedSystem(IEventBus eventBus, IDIContainer diContainer) : base(eventBus)
        {
            this.diContainer = diContainer;
        }

        public void Initialize()
        {
            diContainer.Fetch(this);
        }
        
        protected override void Execute(List<LoaderStepFinishedEvent> events)
        {
            for (int i = 0; i < events.Count; i++)
            {
                events[i].IsDestroyed = true;
            }
            
            if (loaderQueue.IsStarted && loaderQueue.StepInProgress == null && !TryStartNextStep())
            {
                eventBus.Fire<LoaderFinishedEvent>(new LoaderFinishedEvent());
            }
        }
        
        private bool TryStartNextStep()
        {
            for (int i = 0; i < loaderQueue.Steps.Count; i++)
            {
                var loaderStep = loaderQueue.Steps[i];
                if (!loaderStep.IsFinished)
                {
                    loaderQueue.StepInProgress = loaderStep;
                    loaderStep.PerformStep();
                    if (!loaderStep.IsReady()) return true;
                    else loaderStep.FinishStep();
                }
            }
            return false;
        }
    }
}