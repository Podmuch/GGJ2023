using PDGames.DIContainer;
using PDGames.EventBus;
using PDGames.Systems;
using UnityEngine;
using UnityEngine.UI;

namespace PDGames.UserInterface
{
    public sealed class UiCanvasInitializationSystem : IInitializeSystem
    {
        [DIInject]
        private UiConfig uiConfig = default;
        [DIInject]
        private UiHolder uiHolder = default;
        
        private IDIContainer diContainer;
        
        public UiCanvasInitializationSystem(IEventBus eventBus, IDIContainer diContainer)
        {
            this.diContainer = diContainer;
        }

        public void Initialize()
        {
            diContainer.Fetch(this);
            CreateCanvasRoot();
            CreateUiCanvas();
        }

        private void CreateCanvasRoot()
        {
            var rootGameObject = new GameObject(uiConfig.UiRootName);
            GameObject.DontDestroyOnLoad(rootGameObject);
            uiHolder.CanvasRoot = rootGameObject;
            ResetTransform(rootGameObject.transform);
        }

        private void CreateUiCanvas()
        {
            var canvasGameObject = new GameObject("UICanvas");
            canvasGameObject.transform.SetParent(uiHolder.CanvasRoot.transform);

            var canvasComponent = canvasGameObject.AddComponent<Canvas>();
            canvasComponent.renderMode = uiConfig.RenderMode;

            var canvasScaler = canvasGameObject.AddComponent<CanvasScaler>();
            canvasScaler.uiScaleMode = uiConfig.ScaleMode;
            canvasScaler.referenceResolution = uiConfig.ReferenceResolution;
            canvasScaler.screenMatchMode = uiConfig.ScreenMatchMode;
            canvasScaler.matchWidthOrHeight = uiConfig.MatchWidthOrHeight;

            canvasGameObject.AddComponent<GraphicRaycaster>();
            
            ResetTransform(canvasGameObject.transform);
            uiHolder.WindowsParent = canvasGameObject.transform;
        }

        private void ResetTransform(Transform transform)
        {
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.identity;
            transform.localScale = Vector3.one;
        }
    }
}