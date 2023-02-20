using PDGames.DIContainer;
using PDGames.EventBus;
using BoxColliders.Game;
using BoxColliders.Windows;
using Controllers;
using PDGames.Systems.Loader;
using StemSystem;
using UnityEngine;
using Utils;

namespace BoxColliders.Project
{
    public sealed class TransitionLoadGameControllersStep : LoaderStep
    {
        [DIInject] 
        private GameplayObjectsPool objectsPool = default;

        private string controllersPath = "Controllers/";
        
        private string playerControllerPath = "PlayerController";
        
        
        private IDIContainer diContainer;

        private bool isReady = false;
        
        public TransitionLoadGameControllersStep(IEventBus eventBus, IDIContainer diContainer) : base(eventBus)
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
            return DefinedLocaleKeys.LoadingGameplay;
        }

        public override void PerformStep()
        {
            var diContext = diContainer.GetReference<GameplayContextsHolder>(null).GameContext;
            diContainer.Fetch(this, diContext);

            var controllersParent = MonoBehaviour.Instantiate(new GameObject());
            controllersParent.name = "GameplayControllers";
            diContainer.Register(controllersParent, diContext);
            
            var playerControllerPrefab = Resources.Load<PlayerController>(controllersPath + playerControllerPath);
            var playerController = MonoBehaviour.Instantiate(playerControllerPrefab, controllersParent.transform);
            playerController.Initialize(diContainer, diContext);
            
            var cameraFollowScript = MainCameraController.Instance.GetComponent<CameraFollow>();
            cameraFollowScript.carTransform = playerController.transform;
                
            diContainer.Register(playerController, diContext);
            
            
            isReady = true;
        }
    }
}