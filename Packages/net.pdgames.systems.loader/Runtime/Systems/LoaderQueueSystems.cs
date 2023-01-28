using PDGames.DIContainer;
using PDGames.EventBus;

namespace PDGames.Systems.Loader
{
    public sealed class LoaderQueueSystems : SystemsCascadeData
    {
        public LoaderQueueSystems(IEventBus eventBus, IDIContainer diContainer)
        {
            Add(new InitializeLoaderQueueSystem(eventBus, diContainer));
            Add(new ProcessLoaderStartSystem(eventBus, diContainer));
            Add(new ClearLoaderStepInProgressChangedEventSystem(eventBus));
            Add(new ProcessLoaderStepFinishedSystem(eventBus, diContainer));
            Add(new ProcessLoaderFinishSystem(eventBus, diContainer));
            Add(new ClearOldLoaderProgressSystem(eventBus));
            Add(new UpdateLoaderProgressSystem(eventBus, diContainer));
            Add(new FinishStepIfReadySystem(eventBus, diContainer));
            Add(new ClearLoaderSystem(eventBus, diContainer));
        }
    }
}