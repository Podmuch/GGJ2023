using PDGames.DIContainer;
using PDGames.EventBus;
using PDGames.Systems;

namespace BoxColliders.Project
{
    public sealed class ProjectInitializationSystems : SystemsCascadeData
    {
        public ProjectInitializationSystems(IEventBus eventBus, IDIContainer diContainer)
        {
            Add(new ConfigInitializeGameplayConfigSystem(eventBus, diContainer));
            Add(new ConfigInitializeArenasConfigSystem(eventBus, diContainer));
            Add(new ConfigInitializeAudioConfigSystem(eventBus, diContainer));
            Add(new ConfigInitializeDayNightCycleConfigSystem(eventBus, diContainer));
            Add(new ConfigInitializeRainCloudConfigSystem(eventBus, diContainer));
            Add(new ConfigInitializeSmogCloudConfigSystem(eventBus, diContainer));
            Add(new ConfigInitializeResourcesConfigSystem(eventBus, diContainer));
        }
    }
}