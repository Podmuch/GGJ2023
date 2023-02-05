using UnityEngine;

namespace BoxColliders.Project
{
    public sealed class MainCameraController : MonoBehaviour
    {
        public Camera MainCamera = default;
        public Animator Animator = default;
        
        private static MainCameraController Instance { get; set; }

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