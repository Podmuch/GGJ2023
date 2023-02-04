using PDGames.DIContainer;
using PDGames.EventBus;
using PDGames.Systems;

namespace BoxColliders.Game
{
    public sealed class GameInitializeBranchIndicatorDataSystem : IInitializeSystem
    {
        private IEventBus eventBus;
        private IDIContainer diContainer;
        private object diContext;
        
        public GameInitializeBranchIndicatorDataSystem(IEventBus eventBus, IDIContainer diContainer, object diContext)
        {
            this.eventBus = eventBus;
            this.diContainer = diContainer;
            this.diContext = diContext;
        }

        public void Initialize()
        {
            diContainer.Fetch(this, diContext);
            var branchIndicatorData = new GameplayBranchIndicatorData();
            diContainer.Register(branchIndicatorData, diContext);
        }
    }
}