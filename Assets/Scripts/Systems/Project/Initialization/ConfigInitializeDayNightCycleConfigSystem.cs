using System.Collections.Generic;
using BoxColliders.Configs;
using PDGames.DIContainer;
using PDGames.EventBus;
using PDGames.Systems;
using UnityEngine;

namespace BoxColliders.Project
{
    public sealed class ConfigInitializeDayNightCycleConfigSystem : ReactiveSystem<InitializeConfigurationEvent>, IInitializeSystem
    {
        private IDIContainer diContainer;
        
        public ConfigInitializeDayNightCycleConfigSystem(IEventBus eventBus, IDIContainer diContainer) : base(eventBus)
        {
            this.diContainer = diContainer;
        }

        public void Initialize()
        {
        }
        
        protected override void Execute(List<InitializeConfigurationEvent> events)
        {
            for (int i = 0; i < events.Count; i++)
            {
                events[i].IsProcessed = true;
            }
            
            LoadConfig();
        }

        private void LoadConfig()
        {
            var dayNightCycle = Resources.Load<GameplayDayNightCycleConfig>(GameplayDayNightCycleConfig.ResourcePath);
            if (dayNightCycle == null)
            {
                Debug.LogError("[Config] SunConfig not found at path: " + GameplayDayNightCycleConfig.ResourcePath);
            }
            else
            {
                diContainer.Register(dayNightCycle);
            }
        }
    }
}