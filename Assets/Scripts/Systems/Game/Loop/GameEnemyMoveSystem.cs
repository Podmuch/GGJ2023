using BoxColliders.Project;
using Controllers;
using PDGames.DIContainer;
using PDGames.EventBus;
using PDGames.Systems;
using UnityEngine;

namespace BoxColliders.Game
{
    public sealed class GameEnemyMoveSystem : IInitializeSystem, IExecuteSystem
    {
        [DIInject] 
        private GameplayStateData stateData = default;
        [DIInject]
        private GameplayInputData inputData = default;
        [DIInject]
        private PlayerController playerController = default;
        [DIInject]
        private EnemyController enemyController = default;

        private Transform cameraTransform;
        
        private IDIContainer diContainer;
        private object diContext;
        
        public GameEnemyMoveSystem(IEventBus eventBus, IDIContainer diContainer, object diContext)
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
            LookAtPlayer();
        }
        
        private void LookAtPlayer()
        {
            enemyController.transform.LookAt(playerController.transform);
        }
    }
}