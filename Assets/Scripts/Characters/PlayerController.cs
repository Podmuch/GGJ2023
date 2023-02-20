using PDGames.DIContainer;
using UnityEngine;

namespace Controllers
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private CharacterMovementController characterMovementController;

        public CharacterMovementController CharacterMovementController => characterMovementController;
        
        
        private IDIContainer diContainer;
        private object diContext;

        public void Initialize(IDIContainer diContainer, object diContext)
        {
            this.diContainer = diContainer;
            this.diContext = diContext;
            diContainer.Fetch(this, diContext);
            
            characterMovementController.Initialize(diContainer, diContext);
        }
    }
}