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

            if (!sunData.isMoving) return;

            if (sunData.currentTime <= sunData.currentSunPath.time)
            {
                sunData.currentTime += Time.deltaTime;
                sunData.sunFactor = Mathf.InverseLerp(0, sunData.currentSunPath.time, sunData.currentTime);
            }
            else sunData.isMoving = false;

            var sunPosition = sunData.currentSunPath.Evaluate(sunData.sunFactor);
            sunController.SetPosition(sunPosition);
        }
    }
}