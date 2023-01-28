using UnityEngine;

namespace PDGames.UserInterface
{
    public sealed class AlphaComponent : MonoBehaviour
    {
        [SerializeField] 
        private CanvasGroup canvasGroup;
        
        [SerializeField] 
        private float speed = 1f;
        [SerializeField] 
        private bool isScaledTime = true;

        private void Update()
        {
            float time = isScaledTime ? Time.time : Time.unscaledTime;
            canvasGroup.alpha = Mathf.Abs(Mathf.Sin(time * speed));
        }
    }
}
