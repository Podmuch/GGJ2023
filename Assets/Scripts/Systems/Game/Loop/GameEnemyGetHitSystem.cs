using System.Collections.Generic;
using BoxColliders.Project;
using Controllers;
using PDGames.DIContainer;
using PDGames.EventBus;
using PDGames.Systems;
using UnityEngine;

namespace BoxColliders.Game
{
    public sealed class GameEnemyGetHitSystem : ReactiveSystem<EnemyGetHitEvent>, IInitializeSystem
    {
        [DIInject] 
        private GameplayStateData stateData = default;
        [DIInject]
        private GameplayInputData inputData = default;
        [DIInject]
        private PlayerController playerController = default;
        [DIInject]
        private EnemyController enemyController = default;
        
        private IDIContainer diContainer;
        private object diContext;
        
        public GameEnemyGetHitSystem(IEventBus eventBus, IDIContainer diContainer, object diContext) : base(eventBus)
        {
            this.diContainer = diContainer;
            this.diContext = diContext;
        }

        public void Initialize()
        {
            diContainer.Fetch(this, diContext);
        }
        
        protected override void Execute(List<EnemyGetHitEvent> events)
        {
            var hitEvent = events[0];
            SetGetHitAnimation(hitEvent.PushPower);
        }

        public void SetGetHitAnimation(float pushPower)
        {
            var distance = Vector3.Distance(
                new Vector3(playerController.transform.position.x, 0f, playerController.transform.position.z),
                new Vector3(enemyController.transform.position.x, 0f, enemyController.transform.position.z));
            
            enemyController.CharacterAnimationToRigidbodyController.SetVelocityScaler(pushPower *  (1 / distance));
            enemyController.CharacterGetHitVisualController.GetHitTrigger();
        }
    }
}