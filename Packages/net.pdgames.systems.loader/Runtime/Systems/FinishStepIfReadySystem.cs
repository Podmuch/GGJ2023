using PDGames.DIContainer;
using PDGames.EventBus;

namespace PDGames.Systems.Loader
{
    public sealed class FinishStepIfReadySystem : IExecuteSystem, IInitializeSystem
    {
        [DIInject] private LoaderQueue loaderQueue = default;

        private IDIContainer diContainer;

        public FinishStepIfReadySystem(IEventBus eventBus, IDIContainer diContainer)
        {
            this.diContainer = diContainer;
        }

        public void Initialize()
        {
            diContainer.Fetch(this);
        }

        public void Execute()
        {
            if (loaderQueue.IsStarted && loaderQueue.StepInProgress != null &&
                loaderQueue.StepInProgress.IsReady() && !loaderQueue.StepInProgress.IsFinished)
            {
                loaderQueue.StepInProgress.FinishStep();
                loaderQueue.StepInProgress = null;
            }
        }
    }
}