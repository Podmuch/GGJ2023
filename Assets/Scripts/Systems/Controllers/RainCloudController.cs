using UnityEngine;

namespace BoxColliders.Game
{
    public sealed class RainCloudController : MonoBehaviour
    {
        [SerializeField] 
        private Transform cloudParent; 
        
        public void SetPosition(Vector2 position)
        {
            cloudParent.localPosition = position;
        }
    }
}