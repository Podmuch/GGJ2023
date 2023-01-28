using PDGames.DIContainer;
using PDGames.EventBus;
using PDGames.Systems;

namespace BoxColliders.Project
{
    public sealed class ProjectEventsDestroySystem : IExecuteSystem, IInitializeSystem
    {
        [DIInject] 
        private EventCollector eventCollector = default;

        private IDIContainer diContainer;
        
        public ProjectEventsDestroySystem(IEventBus eventBus, IDIContainer diContainer)
        {
            this.diContainer = diContainer;
        }
        
        public void Initialize()
        {
            diContainer.Fetch(this);
        }

        public void Execute()
        {
            eventCollector.ClearDestroyed();
        }
    }
}