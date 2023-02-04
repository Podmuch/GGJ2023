using PDGames.DIContainer;
using PDGames.EventBus;
using PDGames.Systems;
using UnityEngine;

namespace BoxColliders.Game
{
    public sealed class GameSunSystem : IInitializeSystem, IExecuteSystem
    {
        [DIInject] 
        private GameplayStateData stateData = default;
        [DIInject] 
        private GameplaySunData sunData = default;
        [DIInject] 
        private SunController sunController = default;
        [DIInject] 
        private GameplayDayNightCycleData dayNightCycleData = default;
        
        private IDIContainer diContainer;
        private object diContext;
        
        public GameSunSystem(IEventBus eventBus, IDIContainer diContainer, object diContext)
        {
            this.diContainer = diContainer;
            this.diContext = diContext;
        }

        public void Initialize()
        {
            diContainer.Fetch(this, diContext);
        }

        public void Execute()
        {
            if (!stateData.IsStarted) return;

            if (!dayNightCycleData.isDay) return;
            
            if (dayNightCycleData.currentCycleTime <= dayNightCycleData.endCycleTime)
            {
                sunData.sunFactor = Mathf.InverseLerp(0, dayNightCycleData.endCycleTime, dayNightCycleData.currentCycleTime);
            }

            var sunPosition = sunData.currentSunPath.Evaluate(sunData.sunFactor);
            sunController.SetPosition(sunPosition);
        }
    }
}