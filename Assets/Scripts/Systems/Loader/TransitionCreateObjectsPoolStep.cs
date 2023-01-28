using BoxColliders.Game;
using PDGames.DIContainer;
using PDGames.EventBus;
using PDGames.Systems.Loader;
using UnityEngine;
using Utils;

namespace BoxColliders.Project
{
    public sealed class TransitionCreateObjectsPoolStep : LoaderStep
    {
        [DIInject] 
        private GameplayContextsHolder contextsHolder = default;
        
        private IDIContainer diContainer;

        public TransitionCreateObjectsPoolStep(IEventBus eventBus, IDIContainer diContainer) : base(eventBus)
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
            diContainer.Fetch(this);

            var poolRoot = new GameObject("GameObjectsPool");
            poolRoot.transform.localPosition = Vector3.zero;
            poolRoot.transform.localRotation = Quaternion.identity;
            poolRoot.transform.localScale = Vector3.one;
            
            var objectsPool = poolRoot.AddComponent<GameplayObjectsPool>();
            objectsPool.Root = poolRoot;
            diContainer.Register(objectsPool, contextsHolder.GameContext);
        }
    }
}