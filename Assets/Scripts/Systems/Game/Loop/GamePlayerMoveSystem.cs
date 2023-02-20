using BoxColliders.Project;
using Controllers;
using PDGames.DIContainer;
using PDGames.EventBus;
using PDGames.Systems;
using UnityEngine;

namespace BoxColliders.Game
{
    public sealed class GamePlayerMoveSystem : IInitializeSystem, IExecuteSystem
    {
        [DIInject] 
        private GameplayStateData stateData = default;
        [DIInject]
        private GameplayInputData inputData = default;
        [DIInject]
        private PlayerController playerController = default;

        private Transform cameraTransform;
        
        private IDIContainer diContainer;
        private object diContext;
        
        public GamePlayerMoveSystem(IEventBus eventBus, IDIContainer diContainer, object diContext)
        {
            this.diContainer = diContainer;
            this.diContext = diContext;
        }

        public void Initialize()
        {
            diContainer.Fetch(this, diContext);
            cameraTransform = MainCameraController.Instance.transform;
        }
        

        public void Execute()
        {
            Move();
        }
        
        public void Move()
        {
            var vInput = inputData.JoystickY;
            var hInput = inputData.JoystickX;

            var cameraForward = cameraTransform.forward;
            var cameraRight = cameraTransform.right;
                
            Vector3 move = vInput * cameraForward + hInput * cameraRight;
            
            var playerMoveController = playerController.CharacterMovementController;
            playerMoveController.Move(move);
        }
    }
}