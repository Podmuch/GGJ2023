using BoxColliders.Project;
using UnityEngine;

namespace Controllers
{
    public class PlayerAnimationsEventsHandler : MonoBehaviour
    {
        [SerializeField]
        private Animator animator;
        [SerializeField]
        private Rigidbody rigidBody;

        public void Hit(float pushPower)
        {
            ProjectEventBus.Instance.Fire<EnemyGetHitEvent>(new EnemyGetHitEvent(){PushPower = pushPower});
        }
        
    }
}