using BoxColliders.Game;
using PDGames.DIContainer;
using PDGames.EventBus;
using PDGames.Systems;
using UnityEngine;

namespace BoxColliders.Project
{
    public sealed class ProjectExitGameSystem : IInitializeSystem, IExecuteSystem
    {
        private IDIContainer diContainer;
        private object diContext;
        
        public ProjectExitGameSystem(IEventBus eventBus, IDIContainer diContainer)
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
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                var contextsHolder = diContainer.GetReference<GameplayContextsHolder>(null);
                if (contextsHolder == null)
                {
                    Debug.LogError("QUIT");
                    Application.Quit();
                }
            }
        }
    }
}