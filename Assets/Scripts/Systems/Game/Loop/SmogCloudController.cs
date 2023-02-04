using UnityEngine;

namespace BoxColliders.Game
{
    public class SmogCloudController : MonoBehaviour
    {
        [SerializeField] 
        private Transform cloudParent; 
        
        public void SetPosition(Vector2 position)
        {
            cloudParent.localPosition = position;
        }
    }
}