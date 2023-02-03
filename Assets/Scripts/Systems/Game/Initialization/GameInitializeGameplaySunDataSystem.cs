using PDGames.DIContainer;
using PDGames.EventBus;
using PDGames.Systems;

namespace BoxColliders.Game
{
    public sealed class GameInitializeGameplaySunDataSystem : IInitializeSystem
    {
        private IEventBus eventBus;
        private IDIContainer diContainer;
        private object diContext;
        
        public GameInitializeGameplaySunDataSystem(IEventBus eventBus, IDIContainer diContainer, object diContext)
        {
            this.eventBus = eventBus;
            this.diContainer = diContainer;
            this.diContext = diContext;
        }

        public void Initialize()
        {
            diContainer.Fetch(this, diContext);
            var sunData = new GameplaySunData();
            diContainer.Register(sunData, diContext);
        }
    }
}