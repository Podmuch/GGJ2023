using System.Collections.Generic;
using BoxColliders.Configs;
using PDGames.DIContainer;
using PDGames.EventBus;
using PDGames.Systems;
using UnityEngine;

namespace BoxColliders.Project
{
    public sealed class ConfigInitializeRainCloudConfigSystem: ReactiveSystem<InitializeConfigurationEvent>, IInitializeSystem
    {
        private IDIContainer diContainer;
        
        public ConfigInitializeRainCloudConfigSystem(IEventBus eventBus, IDIContainer diContainer) : base(eventBus)
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
            var rainCloudConfig = Resources.Load<GameplayRainCloudConfig>(GameplayRainCloudConfig.ResourcePath);
            if (rainCloudConfig == null)
            {
                Debug.LogError("[Config] GameplayRainCloudConfig not found at path: " + GameplayRainCloudConfig.ResourcePath);
            }
            else
            {
                diContainer.Register(rainCloudConfig);
            }
        }
    }
}