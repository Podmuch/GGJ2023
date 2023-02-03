using PDGames.DIContainer;
using PDGames.EventBus;
using PDGames.Systems;
using UnityEngine;

namespace PDGames.UserInterface
{
    public sealed class UiConfigInitializationSystem : IInitializeSystem
    {
        private IDIContainer diContainer;
        
        public UiConfigInitializationSystem(IEventBus eventBus, IDIContainer diContainer)
        {
            this.diContainer = diContainer;
        }

        public void Initialize()
        {
            var uiConfig = LoadUiConfig();
            diContainer.Register(uiConfig);
        }

        private UiConfig LoadUiConfig()
        {
            var uiConfig = Resources.Load<UiConfig>(UiConfig.ResourcePath);
            if (uiConfig == null)
            {
                Debug.LogError("[UI] UiConfig not found at path: " + UiConfig.ResourcePath);
            }
            return uiConfig;
        }
    }
}