using System;
using System.Collections.Generic;
using UnityEngine;

namespace PDGames.UserInterface
{
    [Serializable]
    public sealed class UiHolder
    {
        public GameObject CanvasRoot;
        
        public Transform WindowsParent;

        private Dictionary<string, IBaseWindow> loadedWindows = new Dictionary<string, IBaseWindow>();

        public IBaseWindow GetWindow(string windowId)
        {
            if (!string.IsNullOrEmpty(windowId) && loadedWindows.ContainsKey(windowId))
            {
                if (loadedWindows[windowId] == null) loadedWindows.Remove(windowId);
                else return loadedWindows[windowId];
            }
            return null;
        }

        public T GetWindow<T>() where T : IBaseWindow
        {
            var windowId = typeof(T).Name;
            return (T)GetWindow(windowId);
        }

        public void RegisterWindow(string windowId, IBaseWindow window)
        {
            if (!string.IsNullOrEmpty(windowId) && !loadedWindows.ContainsKey(windowId) && window != null)
            {
                loadedWindows.Add(windowId, window);
            }
            else
            {
                Debug.LogError("[UI] Cannot register window with Id=" + windowId + " to UiHolder");
            }
        }
    }
}