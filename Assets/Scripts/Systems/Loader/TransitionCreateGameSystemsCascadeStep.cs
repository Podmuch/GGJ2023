using PDGames.DIContainer;
using PDGames.EventBus;
using BoxColliders.Game;
using PDGames.Systems.Loader;
using UnityEngine;
using Utils;

namespace BoxColliders.Project
{
    public sealed class TransitionCreateGameSystemsCascadeStep : LoaderStep
    {
        private IDIContainer diContainer;

        public TransitionCreateGameSystemsCascadeStep(IEventBus eventBus, IDIContainer diContainer) : base(eventBus)
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
            return DefinedLocaleKeys.LoadingGameplay;
        }

        public override void PerformStep()
        {
            var gameSystemsPrefab = Resources.Load<GameSystemCascadeController>("GameSystemsCascade");
            var gameSystems = GameObject.Instantiate(gameSystemsPrefab);
            diContainer.Register(gameSystems, gameSystems);
            
            var contextsHolder = new GameplayContextsHolder();
            contextsHolder.GameContext = gameSystems;
            diContainer.Register(contextsHolder, null);
        }
    }
}