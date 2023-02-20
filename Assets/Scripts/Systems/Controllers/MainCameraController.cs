using UnityEngine;

namespace BoxColliders.Project
{
    public sealed class MainCameraController : MonoBehaviour
    {
        public Camera MainCamera = default;
        
        public static MainCameraController Instance { get; set; }

        #region MONO BEHAVIOUR
        
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
        
        #endregion
    }
}