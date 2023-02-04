using PDGames.DIContainer;
using PDGames.EventBus;
using PDGames.Systems;
using Systems.Data;

namespace BoxColliders.Game
{
    public sealed class GameInitializeRainCloudDataSystem : IInitializeSystem
    {
        private IEventBus eventBus;
        private IDIContainer diContainer;
        private object diContext;
            
        public GameInitializeRainCloudDataSystem(IEventBus eventBus, IDIContainer diContainer, object diContext)
        {
            this.eventBus = eventBus;
            this.diContainer = diContainer;
            this.diContext = diContext;
        }

        public void Initialize()
        {
            diContainer.Fetch(this, diContext);
            var rainCloudData = new GameplayRainCloudData();
            diContainer.Register(rainCloudData, diContext);
        }
    }
}