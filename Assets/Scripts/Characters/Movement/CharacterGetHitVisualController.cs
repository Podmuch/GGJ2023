using PDGames.DIContainer;
using UnityEngine;
using Utils;

namespace Controllers
{
    public class CharacterGetHitVisualController : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] 
        private Rigidbody _rigidbody;
        [SerializeField] 
        private Animator animator;
        
        [Header("GetHitProperties")] 
        [SerializeField]
        private int amountOfHitAnimations = 2;
        
        private IDIContainer diContainer;
        private object diContext;
        
        public void Initialize(IDIContainer diContainer, object diContext)
        {
            this.diContainer = diContainer;
            this.diContext = diContext;
            diContainer.Fetch(this, diContext);
        }

        private void Update()
        {
        }
        
        
        public void GetHitTrigger()
        {
            var nextID = Random.Range(0, amountOfHitAnimations);
            animator.SetTrigger(DefinedAnimationParams.Hit + nextID);
        }
    }
}
