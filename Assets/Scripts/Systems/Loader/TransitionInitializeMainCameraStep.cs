using PDGames.DIContainer;
using PDGames.EventBus;
using PDGames.Systems.Loader;
using UnityEngine;
using Utils;

namespace BoxColliders.Project
{
    public sealed class TransitionInitializeMainCameraStep : LoaderStep
    {
        private IDIContainer diContainer;

        public TransitionInitializeMainCameraStep(IEventBus eventBus, IDIContainer diContainer) : base(eventBus)
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
            return DefinedLocaleKeys.InterfaceInitialization;
        }

        public override void PerformStep()
        {
            var mainCamera = GameObject.FindObjectOfType<MainCameraController>();
            diContainer.Register(mainCamera);
        }
    }
}