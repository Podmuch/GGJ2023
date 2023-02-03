using BoxColliders.Configs;
using PDGames.DIContainer;
using PDGames.EventBus;
using PDGames.Systems;
using UnityEngine;

namespace BoxColliders.Game
{
    public sealed class GameWaterProductionSystem : IInitializeSystem, IExecuteSystem
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
        
        public GameWaterProductionSystem(IEventBus eventBus, IDIContainer diContainer, object diContext)
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
            if (treeStateData.CurrentWater < gameplayConfig.MaxWaterCapacity)
            {
                for (int i = 0; i < branchesList.Branches.Count; i++)
                {
                    var branch = branchesList.Branches[i];
                    if (branch.CanProduceWater())
                    {
                        var updatedWaterValue = treeStateData.CurrentWater + branch.GetWaterProduction();
                        treeStateData.CurrentWater = Mathf.Clamp(updatedWaterValue, 0, gameplayConfig.MaxWaterCapacity);
                    }
                }
            }
        }
    }
}