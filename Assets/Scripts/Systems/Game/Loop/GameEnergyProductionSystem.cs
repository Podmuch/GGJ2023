using BoxColliders.Configs;
using PDGames.DIContainer;
using PDGames.EventBus;
using PDGames.Systems;
using UnityEngine;

namespace BoxColliders.Game
{
    public class GameEnergyProductionSystem : IInitializeSystem, IExecuteSystem
    {
        [DIInject] 
        private GameplayConfig gameplayConfig;
        [DIInject] 
        private GameTreeStateData treeStateData;
        
        private IEventBus eventBus;
        private IDIContainer diContainer;
        private object diContext;
        
        public GameEnergyProductionSystem(IEventBus eventBus, IDIContainer diContainer, object diContext)
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
            treeStateData.EnergyTimer += Time.deltaTime;
            if (treeStateData.EnergyTimer >= gameplayConfig.EnergyProductionInterval)
            {
                if (IsEnoughResourcesForEnergy())
                {
                    ProduceEnergy();
                    if (treeStateData.Energy >= gameplayConfig.EnergyForGrow)
                    {
                        eventBus.Fire<GameTreeGrowEvent>();
                        treeStateData.Energy -= gameplayConfig.EnergyForGrow;
                    }
                }
                treeStateData.EnergyTimer -= gameplayConfig.EnergyProductionInterval;
            }
        }

        private bool IsEnoughResourcesForEnergy()
        {
            return treeStateData.CurrentWater >= gameplayConfig.WaterToEnergyConversion && 
                treeStateData.CurrentSun >= gameplayConfig.SunToEnergyConversion &&
                treeStateData.CurrentAir >= gameplayConfig.AirToEnergyConversion;
        }

        private void ProduceEnergy()
        {
            treeStateData.Energy++;
            treeStateData.CurrentWater -= gameplayConfig.WaterToEnergyConversion;
            treeStateData.CurrentSun -= gameplayConfig.SunToEnergyConversion;
            treeStateData.CurrentAir -= gameplayConfig.AirToEnergyConversion;
        }
    }
}