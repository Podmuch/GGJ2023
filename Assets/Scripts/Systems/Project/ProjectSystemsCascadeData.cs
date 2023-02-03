using PDGames.DIContainer;
using PDGames.EventBus;
using PDGames.Systems;
using PDGames.Systems.Loader;
using PDGames.UserInterface;

namespace BoxColliders.Project
{
    public sealed class ProjectSystemsCascadeData : SystemsCascadeData
    {
        public ProjectSystemsCascadeData(IEventBus eventBus, IDIContainer diContainer)
        {
            Add(new LoaderQueueSystems(eventBus, diContainer));
            
            Add(new ProjectInitializationSystems(eventBus, diContainer));
            
            Add(new UiHandlingSystems(eventBus, diContainer));
            Add(new UiManagementSystems(eventBus, diContainer));
            Add(new ProjectTransitionSystems(eventBus, diContainer));
            
            Add(new ProjectEventsDestroySystem(eventBus, diContainer));
            Add(new ProjectDeinitializationSystem(eventBus, diContainer));
        }
    }
}