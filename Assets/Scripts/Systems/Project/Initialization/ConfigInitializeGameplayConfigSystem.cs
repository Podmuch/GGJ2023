using System.Collections.Generic;
using BoxColliders.Configs;
using PDGames.DIContainer;
using PDGames.EventBus;
using PDGames.Systems;
using UnityEngine;

namespace BoxColliders.Project
{
    public sealed class ConfigInitializeGameplayConfigSystem : ReactiveSystem<InitializeConfigurationEvent>, IInitializeSystem
    {
        private IDIContainer diContainer;
        
        public ConfigInitializeGameplayConfigSystem(IEventBus eventBus, IDIContainer diContainer) : base(eventBus)
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
            var gameplayConfig = Resources.Load<GameplayConfig>(GameplayConfig.ResourcePath);
            if (gameplayConfig == null)
            {
                Debug.LogError("[Config] GameplayConfig not found at path: " + GameplayConfig.ResourcePath);
            }
            else
            {
                diContainer.Register(gameplayConfig);
            }
        }
    }
}