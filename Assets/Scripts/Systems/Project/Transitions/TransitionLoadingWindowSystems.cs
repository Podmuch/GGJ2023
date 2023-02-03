using PDGames.DIContainer;
using PDGames.EventBus;
using PDGames.Systems;

namespace BoxColliders.Project
{
    public sealed class TransitionLoadingWindowSystems : SystemsCascadeData
    {
        public TransitionLoadingWindowSystems(IEventBus eventBus, IDIContainer diContainer)
        {
            Add(new TransitionShowLoadingWindowSystem(eventBus, diContainer));
            Add(new TransitionUpdateLoadingWindowSystem(eventBus, diContainer));
            Add(new TransitionHideLoadingWindowSystem(eventBus, diContainer));
        }
    }
}