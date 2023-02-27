using PDGames.DIContainer;
using UnityEngine;

namespace Controllers
{
    public class EnemyController : MonoBehaviour
    {
        [SerializeField] private CharacterMovementController characterMovementController;
        [SerializeField] private CharacterGetHitVisualController characterGetHitVisualController;
        [SerializeField] private CharacterAnimationToRigidbodyController characterAnimationToRigidbodyController;
        
        public CharacterMovementController CharacterMovementController => characterMovementController;
        public CharacterGetHitVisualController CharacterGetHitVisualController => characterGetHitVisualController;
        public CharacterAnimationToRigidbodyController CharacterAnimationToRigidbodyController => characterAnimationToRigidbodyController;
        
        
        private IDIContainer diContainer;
        private object diContext;

        public void Initialize(IDIContainer diContainer, object diContext)
        {
            this.diContainer = diContainer;
            this.diContext = diContext;
            diContainer.Fetch(this, diContext);
            
            characterMovementController.Initialize(diContainer, diContext);
            characterGetHitVisualController.Initialize(diContainer, diContext);
        }
    }
}