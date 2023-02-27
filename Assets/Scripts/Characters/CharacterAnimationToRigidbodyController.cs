using UnityEngine;

namespace Controllers
{
    public class CharacterAnimationToRigidbodyController : MonoBehaviour
    {
        [SerializeField]
        private Animator animator;
        [SerializeField]
        private Rigidbody rigidBody;
        
        private float velocityVectorScaler = 1;

        public void OnAnimatorMove()
        {
            if (Time.deltaTime > 0)
            {
                Vector3 v = (animator.deltaPosition) / Time.deltaTime;


                v.y = rigidBody.velocity.y;
                rigidBody.velocity = v * velocityVectorScaler;
            }
        }

        public void SetVelocityScaler(float scaler)
        {
            velocityVectorScaler = scaler;
        }
    }
}