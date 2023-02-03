using PDGames.DIContainer;
using PDGames.EventBus;
using PDGames.Systems;

namespace BoxColliders.Project
{
    public sealed class ProjectDeinitializationSystem : SystemsCascadeData
    {
        public ProjectDeinitializationSystem(IEventBus eventBus, IDIContainer diContainer)
        {
        }
    }
}