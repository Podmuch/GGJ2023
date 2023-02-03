using System.Collections.Generic;
using BoxColliders.Configs;
using PDGames.DIContainer;
using PDGames.EventBus;
using PDGames.Systems;
using UnityEngine;

namespace BoxColliders.Project
{
    public sealed class ConfigInitializeSunConfigSystem : ReactiveSystem<InitializeConfigurationEvent>, IInitializeSystem
    {
        private IDIContainer diContainer;
        
        public ConfigInitializeSunConfigSystem(IEventBus eventBus, IDIContainer diContainer) : base(eventBus)
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
            var sunConfig = Resources.Load<GameplaySunConfig>(GameplaySunConfig.ResourcePath);
            if (sunConfig == null)
            {
                Debug.LogError("[Config] SunConfig not found at path: " + GameplaySunConfig.ResourcePath);
            }
            else
            {
                diContainer.Register(sunConfig);
            }
        }
    }
}