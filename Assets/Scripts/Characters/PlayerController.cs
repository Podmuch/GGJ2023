using PDGames.DIContainer;
using UnityEngine;

namespace Controllers
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private CharacterMovementController characterMovementController;
        [SerializeField] public PlayerCharacterInputController playerInputController;

        public CharacterMovementController CharacterMovementController => characterMovementController;
        public PlayerCharacterInputController PlayerInputController => playerInputController;

        

        private IDIContainer diContainer;
        private object diContext;

        public void Initialize(IDIContainer diContainer, object diContext)
        {
            this.diContainer = diContainer;
            this.diContext = diContext;
            diContainer.Fetch(this, diContext);
            
            playerInputController.Initialize(diContainer, diContext);
            characterMovementController.Initialize(diContainer, diContext);
        }
    }
}