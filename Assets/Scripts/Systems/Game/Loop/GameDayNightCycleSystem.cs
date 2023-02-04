using BoxColliders.Configs;
using BoxColliders.Project;
using PDGames.DIContainer;
using PDGames.EventBus;
using PDGames.Systems;
using UnityEngine;
using Utils;

namespace BoxColliders.Game
{
    public sealed class GameDayNightCycleSystem : IInitializeSystem, IExecuteSystem
    {
        [DIInject] private GameplayStateData stateData = default;
        [DIInject] private GameplayDayNightCycleData dayNightCycleData = default;
        [DIInject] private GameplayDayNightCycleConfig dayNightCycleConfig = default;
        [DIInject] private BackgroundSkyController backgroundController = default;
        [DIInject] private AudioController audioController = default;

        private IDIContainer diContainer;
        private object diContext;

        public GameDayNightCycleSystem(IEventBus eventBus, IDIContainer diContainer, object diContext)
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

            if (dayNightCycleData.currentCycleTime <= dayNightCycleData.endCycleTime)
                dayNightCycleData.currentCycleTime += Time.deltaTime;
            else
            {
                dayNightCycleData.isDay = !dayNightCycleData.isDay;
                var randomMinMaxTime = dayNightCycleData.isDay
                    ? dayNightCycleConfig.minMaxDayTime
                    : dayNightCycleConfig.minMaxNightTime;
                
                dayNightCycleData.endCycleTime = Random.Range(randomMinMaxTime.x, randomMinMaxTime.y);
                if(dayNightCycleData.isDay) ProjectEventBus.Instance.Fire<SetNewSunDataEvent>();
                dayNightCycleData.currentCycleTime = 0f;
                
                audioController.PlayAudio(DefinedAudioKeys.dayStartEnd);
                backgroundController.SetAnimatorBool(dayNightCycleData.isDay);
                
            }
        }
    }
}