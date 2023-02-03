using PDGames.DIContainer;
using PDGames.EventBus;
using PDGames.Systems;

namespace BoxColliders.Project
{
    public sealed class ProjectTransitionSystems : SystemsCascadeData
    {
        public ProjectTransitionSystems(IEventBus eventBus, IDIContainer diContainer)
        {
            Add(new TransitionInitialLoadingSystem(eventBus, diContainer));
            Add(new TransitionStartGameSystem(eventBus, diContainer));
            Add(new TransitionEndGameSystem(eventBus, diContainer));
            
            Add(new TransitionLoadingWindowSystems(eventBus, diContainer));
        }
    }
}