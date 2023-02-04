using System.Collections.Generic;
using BoxColliders.Configs;
using PDGames.DIContainer;
using PDGames.EventBus;
using UnityEngine;

namespace BoxColliders.Game
{
    public sealed class GameBranchController : MonoBehaviour
    {
        [SerializeField] 
        private List<Transform> branchSlots;
        
        [DIInject]
        private GameplayConfig gameplayConfig;
        
        private GameBranchStateData stateData = new GameBranchStateData();

        private IEventBus eventBus;
        private IDIContainer diContainer;
        private object diContext;
        
        public void Initialize(IEventBus eventBus, IDIContainer diContainer, object diContext)
        {
            this.eventBus = eventBus;
            this.diContainer = diContainer;
            this.diContext = diContext;
            
            diContainer.Fetch(this, diContext);
        }
        
        public bool CanProduceWater()
        {
            return stateData.State == BranchState.Water;
        }

        public float GetWaterProduction()
        {
            return gameplayConfig.BranchWaterProduction * Time.deltaTime;
        }

        public bool CanProduceAir()
        {
            return stateData.State == BranchState.Air;
        }

        public float GetAirProduction()
        {
            return gameplayConfig.BranchAirProduction * Time.deltaTime;
        }

        public bool CanProduceSun()
        {
            return stateData.State == BranchState.Sun;
        }

        public float GetSunProduction()
        {
            return gameplayConfig.BranchSunProduction * Time.deltaTime;
        }
        
        public List<Transform> GetBranchSlots()
        {
            return branchSlots;
        }
    }
}