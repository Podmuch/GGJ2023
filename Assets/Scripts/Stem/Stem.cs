using System;
using Services;
using UnityEngine;

namespace StemSystem
{
    public class Stem : MonoBehaviour
    {
        private  static ServicesManager ServiceManager;
        public static UIService UI
        {
            get => GetService<UIService>();
        }

        #region MonoBehaviour

        private void Awake()
        {
            ServiceManager = new ServicesManager();
            ServiceManager.Initialize();
            
            DontDestroyOnLoad(this.gameObject);
        }

        private void OnDestroy()
        {
            ServiceManager.Deinitialize();
        }

        #endregion
        
        public static T GetService<T>() where T : class
        {
            return ServiceManager.GetService<T>();
        }
    }
}
