using BoxColliders.Configs;
using PDGames.DIContainer;
using PDGames.EventBus;
using PDGames.Systems;
using UnityEngine;

namespace BoxColliders.Game
{
    public sealed class GameSunProductionSystem : IInitializeSystem, IExecuteSystem
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
        
        public GameSunProductionSystem(IEventBus eventBus, IDIContainer diContainer, object diContext)
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
            if (treeStateData.CurrentSun < gameplayConfig.MaxSunCapacity)
            {
                for (int i = 0; i < branchesList.Branches.Count; i++)
                {
                    var branch = branchesList.Branches[i];
                    if (branch.CanProduceSun())
                    {
                        var updatedSunValue = treeStateData.CurrentSun + branch.GetSunProduction();
                        treeStateData.CurrentSun = Mathf.Clamp(updatedSunValue, 0, gameplayConfig.MaxSunCapacity);
                    }
                }
            }
        }
    }
}