using System.Collections.Generic;
using BoxColliders.Configs;
using PDGames.DIContainer;
using PDGames.EventBus;
using PDGames.Systems;
using UnityEngine;

namespace BoxColliders.Project
{
    public class ConfigInitializeSmogCloudConfigSystem : ReactiveSystem<InitializeConfigurationEvent>, IInitializeSystem
    {
        private IDIContainer diContainer;
        
        public ConfigInitializeSmogCloudConfigSystem(IEventBus eventBus, IDIContainer diContainer) : base(eventBus)
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
            var smogCloudConfig = Resources.Load<GameplaySmogCloudConfig>(GameplaySmogCloudConfig.ResourcePath);
            if (smogCloudConfig == null)
            {
                Debug.LogError("[Config] GameplaySmogCloudConfig not found at path: " + GameplaySmogCloudConfig.ResourcePath);
            }
            else
            {
                diContainer.Register(smogCloudConfig);
            }
        }
    }
}