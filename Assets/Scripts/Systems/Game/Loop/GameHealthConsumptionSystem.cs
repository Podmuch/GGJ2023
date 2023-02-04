using BoxColliders.Configs;
using PDGames.DIContainer;
using PDGames.EventBus;
using PDGames.Systems;

namespace BoxColliders.Game
{
    public class GameHealthConsumptionSystem : IInitializeSystem, IExecuteSystem
    {
        [DIInject] 
        private GameplayConfig gameplayConfig;
        [DIInject] 
        private GameTreeStateData treeStateData;
        [DIInject] 
        private GameBranchesList branchesList;
            
        private IEventBus eventBus;
        private IDIContainer diContainer;
        private object diContext;
            
        public GameHealthConsumptionSystem(IEventBus eventBus, IDIContainer diContainer, object diContext)
        {
            this.eventBus = eventBus;
            this.diContainer = diContainer;
            this.diContext = diContext;
        }

        public void Initialize()
        {
            diContainer.Fetch(this, diContext);
        }

        public void Execute()
        {
            for (int i = 0; i < branchesList.Branches.Count; i++)
            {
                var branch = branchesList.Branches[i];
                if (branch.CanConsumeHealth())
                {
                    branch.ConsumeHealth();
                }
            }
        }
    }
}