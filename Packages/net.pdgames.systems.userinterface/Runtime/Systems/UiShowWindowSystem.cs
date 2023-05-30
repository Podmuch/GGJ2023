using System.Collections.Generic;
using PDGames.DIContainer;
using PDGames.EventBus;
using PDGames.Systems;
using UnityEngine;

namespace PDGames.UserInterface
{
    public sealed class UiShowWindowSystem : ReactiveSystem<UiShowWindowEvent>, IInitializeSystem
    {
        [DIInject]
        private UiHolder uiHolder = default;
        [DIInject]
        private UiConfig uiConfig = default;
        
        private IDIContainer diContainer;
        
        public UiShowWindowSystem(IEventBus eventBus, IDIContainer diContainer) : base(eventBus)
        {
            this.diContainer = diContainer;
        }

        public void Initialize()
        {
            diContainer.Fetch(this);
        }

        protected override void Execute(List<UiShowWindowEvent> events)
        {
            for (int i = 0; i < events.Count; i++)
            {
                string windowId = !string.IsNullOrEmpty(events[i].Id) ? events[i].Id : events[i].Type.Name;
                ShowWindow(windowId, events[i].OnlyLoadWindow);
                events[i].IsDestroyed = true;
            }
        }

        private void ShowWindow(string windowId, bool onlyLoadWindow)
        {
            var windowToShow = uiHolder.GetWindow(windowId);
            
            if (windowToShow != null && !onlyLoadWindow)
            {
                windowToShow.Show();
            }
            else if(windowToShow == null && !onlyLoadWindow)
            {
                windowToShow = LoadWindow(windowId);
                if (windowToShow != null) windowToShow.Show();
                else
                {
                    Debug.LogError("[UI] Cannot load window with Id=" + windowId + " prefab not found");
                }
            }
            else if(windowToShow == null)
            {
                windowToShow = LoadWindow(windowId);
                if (windowToShow == null)
                    Debug.LogError("[UI] Cannot load window with Id=" + windowId + " prefab not found");
            }
        }

        private IBaseWindow LoadWindow(string windowId)
        {
            string prefabPath = uiConfig.WindowsPath + windowId;
            var windowPrefab = Resources.Load<GameObject>(prefabPath);
            
            if (windowPrefab != null)
            {
                var windowInstance = GameObject.Instantiate(windowPrefab, uiHolder.WindowsParent);
                ResetTransform(windowInstance.transform);
                var baseWindow = windowInstance.GetComponent<IBaseWindow>();
                baseWindow.Initialize(eventBus, diContainer);
                uiHolder.RegisterWindow(windowId, baseWindow);
                return baseWindow;
            }
            return null;
        }

        private void ResetTransform(Transform transform)
        {
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.identity;
            transform.localScale = Vector3.one;
        }
    }
}