using System.Collections.Generic;
using BoxColliders.Configs;
using PDGames.DIContainer;
using PDGames.EventBus;
using PDGames.Systems;
using UnityEngine;

namespace BoxColliders.Project
{
    public sealed class ConfigInitializeResourcesConfigSystem: ReactiveSystem<InitializeConfigurationEvent>, IInitializeSystem
    {
        private IDIContainer diContainer;
        
        public ConfigInitializeResourcesConfigSystem(IEventBus eventBus, IDIContainer diContainer) : base(eventBus)
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
            var resourcesConfig = Resources.Load<GameResourcesConfig>(GameResourcesConfig.ResourcePath);
            if (resourcesConfig == null)
            {
                Debug.LogError("[Config] GameResourcesConfig not found at path: " + GameResourcesConfig.ResourcePath);
            }
            else
            {
                diContainer.Register(resourcesConfig);
            }
        }
    }
}