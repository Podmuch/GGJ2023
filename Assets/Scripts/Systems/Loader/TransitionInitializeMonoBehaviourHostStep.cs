using PDGames.DIContainer;
using PDGames.EventBus;
using PDGames.Systems.Loader;
using UnityEngine;
using Utils;

namespace BoxColliders.Project
{
    public sealed class TransitionInitializeMonoBehaviourHostStep: LoaderStep
    {
        private IDIContainer diContainer;

        private bool isReady = false;
        
        public TransitionInitializeMonoBehaviourHostStep(IEventBus eventBus, IDIContainer diContainer) : base(eventBus)
        {
            this.diContainer = diContainer;
        }

        public override bool IsReady()
        {
            return isReady;
        }

        public override float GetProgress()
        {
            return isReady ? 1.0f : 0.0f;
        }

        public override string GetDescription()
        {
            return DefinedLocaleKeys.GameInitialization;
        }

        public override void PerformStep()
        {
            var newGameObject = new GameObject("MonoBehaviourHost");
            var monoBehaviourHost = newGameObject.AddComponent<MonoBehaviourHost>();
            diContainer.Register(monoBehaviourHost);
            GameObject.DontDestroyOnLoad(newGameObject);
            isReady = true;
        }
    }
}