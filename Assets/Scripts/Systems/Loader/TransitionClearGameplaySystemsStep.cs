using PDGames.DIContainer;
using PDGames.EventBus;
using BoxColliders.Game;
using PDGames.Systems.Loader;
using UnityEngine;
using Utils;

namespace BoxColliders.Project
{
    public sealed class TransitionClearGameplaySystemsStep : LoaderStep
    {
        private IDIContainer diContainer;

        public TransitionClearGameplaySystemsStep(IEventBus eventBus, IDIContainer diContainer) : base(eventBus)
        {
            this.diContainer = diContainer;
        }

        public override bool IsReady()
        {
            return true;
        }

        public override float GetProgress()
        {
            return 1.0f;
        }
        
        public override string GetDescription()
        {
            return DefinedLocaleKeys.LoadingMenu;
        }

        public override void PerformStep()
        {
            var diContext = diContainer.GetReference<GameplayContextsHolder>(null).GameContext;
            
            var gameplayObjectsPool = diContainer.GetReference<GameplayObjectsPool>(diContext);
            if (gameplayObjectsPool != null)
            {
                gameplayObjectsPool.Clear();
                diContainer.Unregister(typeof(GameplayObjectsPool));
                GameObject.Destroy(gameplayObjectsPool.gameObject);
            }
            
            var gameSystemCascade = diContainer.GetReference<GameSystemCascadeController>(diContext);
            if (gameSystemCascade != null)
            {
                diContainer.Unregister(typeof(GameSystemCascadeController));
                gameSystemCascade.Deinitialize();
                GameObject.Destroy(gameSystemCascade.gameObject); 
            }
        }
    }
}