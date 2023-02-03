using PDGames.DIContainer;
using PDGames.EventBus;
using PDGames.Systems.Loader;
using UnityEngine;
using UnityEngine.EventSystems;
using Utils;

namespace BoxColliders.Project
{
    public sealed class TransitionInitializeEventSystemStep : LoaderStep
    {
        private IDIContainer diContainer;

        private bool isReady = false;
        
        public TransitionInitializeEventSystemStep(IEventBus eventBus, IDIContainer diContainer) : base(eventBus)
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
            var eventSystemPath = "EventSystem";
            var eventSystemPrefab = Resources.Load<GameObject>(eventSystemPath);
            if (eventSystemPrefab != null)
            {
                var eventSystemGameObject = GameObject.Instantiate(eventSystemPrefab);
                var eventSystem = eventSystemGameObject.GetComponent<EventSystem>();
                diContainer.Register(eventSystem);
                GameObject.DontDestroyOnLoad(eventSystemGameObject);
                isReady = true;
            }
            else
            {
                Debug.LogError("[Config] EventSystem not found at path: " + eventSystemPath);
            }
        }
    }
}