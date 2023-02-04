using System.Collections.Generic;
using BoxColliders.Configs;
using PDGames.DIContainer;
using PDGames.EventBus;
using PDGames.Systems;
using UnityEngine;

namespace BoxColliders.Project
{
    public sealed class ConfigInitializeAudioConfigSystem : ReactiveSystem<InitializeConfigurationEvent>, IInitializeSystem
    {
        private IDIContainer diContainer;
        
        public ConfigInitializeAudioConfigSystem(IEventBus eventBus, IDIContainer diContainer) : base(eventBus)
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
            var audioConfig = Resources.Load<AudioConfig>(AudioConfig.ResourcePath);
            if (audioConfig == null)
            {
                Debug.LogError("[Config] Audio not found at path: " + AudioConfig.ResourcePath);
            }
            else
            {
                diContainer.Register(audioConfig);
            }
        }
    }
}