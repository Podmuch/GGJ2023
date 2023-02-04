using System.Collections.Generic;
using BoxColliders.Configs;
using BoxColliders.Project;
using PDGames.DIContainer;
using PDGames.EventBus;
using PDGames.Systems;
using Random = UnityEngine.Random;

namespace BoxColliders.Game
{
    public sealed class GameSetNewSunDataReactSystem : ReactiveSystem<SetNewSunDataEvent>, IInitializeSystem
    {
        [DIInject] 
        private GameplayStateData stateData = default;
        [DIInject] 
        private SunController sunController = default;
        [DIInject] 
        private GameplaySunData sunData = default;
        [DIInject] 
        private GameplayDayNightCycleConfig dayNightCycleConfig = default;
        
        private IDIContainer diContainer;
        private object diContext;
        
        public GameSetNewSunDataReactSystem(IEventBus eventBus, IDIContainer diContainer, object diContext) : base(eventBus)
        {
            this.diContainer = diContainer;
            this.diContext = diContext;
        }

        public void Initialize()
        {
            diContainer.Fetch(this, diContext);
        }
        
        protected override void Execute(List<SetNewSunDataEvent> events)
        {
            for (int i = 0; i < events.Count; i++)
            {
                events[i].IsDestroyed = true;
            }
            sunData.currentSunPath = dayNightCycleConfig.sunPaths[Random.Range(0, dayNightCycleConfig.sunPaths.Count)];
            sunData.sunFactor = 0f;
        }
    }
}