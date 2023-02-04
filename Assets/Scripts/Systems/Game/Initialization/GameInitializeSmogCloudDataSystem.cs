using PDGames.DIContainer;
using PDGames.EventBus;
using PDGames.Systems;
using Systems.Data;

namespace BoxColliders.Game
{
    public class GameInitializeSmogCloudDataSystem : IInitializeSystem
    {
        private IEventBus eventBus;
        private IDIContainer diContainer;
        private object diContext;
            
        public GameInitializeSmogCloudDataSystem(IEventBus eventBus, IDIContainer diContainer, object diContext)
        {
            this.eventBus = eventBus;
            this.diContainer = diContainer;
            this.diContext = diContext;
        }

        public void Initialize()
        {
            diContainer.Fetch(this, diContext);
            var smogCloudData = new GameplaySmogCloudData();
            diContainer.Register(smogCloudData, diContext);
        }
    }
}