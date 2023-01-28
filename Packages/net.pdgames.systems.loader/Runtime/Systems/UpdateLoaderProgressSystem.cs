using PDGames.DIContainer;
using PDGames.EventBus;

namespace PDGames.Systems.Loader
{
    public sealed class UpdateLoaderProgressSystem : IExecuteSystem, IInitializeSystem
    {
        [DIInject] 
        private LoaderQueue loaderQueue = default;

        private IEventBus eventBus;
        private IDIContainer diContainer;
        
        public UpdateLoaderProgressSystem(IEventBus eventBus, IDIContainer diContainer)
        {
            this.eventBus = eventBus;
            this.diContainer = diContainer;
        }

        public void Initialize()
        {
            diContainer.Fetch(this);
        }

        public void Execute()
        {
            if (loaderQueue.IsStarted && loaderQueue.StepInProgress != null)
            {
                float progressValue = GetLoaderProgress(out int currentStepIndex);
                string description = loaderQueue.StepInProgress.GetDescription();
                eventBus.Fire<LoaderProgressEvent>(new LoaderProgressEvent()
                {
                    Progress = progressValue,
                    Description = description,
                    CurrentStepIndex = currentStepIndex + 1,
                    TotalSteps = loaderQueue.Steps.Count
                });
            }
        }

        private float GetLoaderProgress(out int currentStepIndex)
        {
            float progressPerStep = 1.0f / (float)loaderQueue.Steps.Count;
            float loaderProgress = 0;
            currentStepIndex = 0;
            for (int i = 0; i < loaderQueue.Steps.Count; i++)
            {
                if (loaderQueue.Steps[i].IsFinished)
                {
                    loaderProgress += progressPerStep;
                }
                else
                {
                    currentStepIndex = i;
                    loaderProgress += progressPerStep * loaderQueue.StepInProgress.GetProgress();
                    break;
                }
            }
            return loaderProgress;
        }
    }
}