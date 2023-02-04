using BoxColliders.Configs;
using PDGames.DIContainer;
using PDGames.EventBus;
using PDGames.Systems;
using Systems.Data;
using UnityEngine;

namespace BoxColliders.Game
{
    public sealed class GameSmogCloudSystem : IInitializeSystem, IExecuteSystem
    {
        [DIInject] 
        private GameplayStateData stateData = default;
        [DIInject] 
        private GameplaySmogCloudData smogCloudData = default;
        [DIInject] 
        private SmogCloudController smogCloudController = default;
        [DIInject] 
        private GameplaySmogCloudConfig smogCloudConfig = default;
        [DIInject] 
        private GameBranchesList gameBranchesList = default;
        
        private IEventBus evenBus;
        private IDIContainer diContainer;
        private object diContext;

        private bool smogActivated = false;
        
        public GameSmogCloudSystem(IEventBus eventBus, IDIContainer diContainer, object diContext)
        {
            this.evenBus = eventBus;
            this.diContainer = diContainer;
            this.diContext = diContext;
        }
        
        public void Initialize()
        {
            diContainer.Fetch(this, diContext);
        }

        public void Execute()
        {
            if (!stateData.IsStarted) return;

            smogActivated = smogActivated || gameBranchesList.Branches.Count >= smogCloudConfig.MinBranchesForSmog;
            if (!smogCloudData.IsWaiting && !smogActivated)
            {
                StartWaiting();
            }
            
            if (!smogCloudData.IsWaiting && !smogCloudData.IsMoving)
            {
                bool startFromMoving = Random.Range(0.0f, 1.0f) > 0.5f;
                if (startFromMoving) StartMoving();
                else StartWaiting();
            }
            else if (smogCloudData.IsWaiting)
            {
                UpdateWaiting();
            }
            else
            {
                UpdateMoving();
            }
        }

        private void StartMoving()
        {
            smogCloudData.IsWaiting = false;
            smogCloudData.IsMoving = true;

            smogCloudData.MovingTimer = 0;
            smogCloudData.MovementDuration = Random.Range(smogCloudConfig.MinMovingTime, smogCloudConfig.MaxMovingTime);

            bool startFromLeft = Random.Range(0.0f, 1.0f) > 0.5f;
            if (startFromLeft)
            {
                smogCloudData.StartPosition = smogCloudConfig.LeftPosition;
                smogCloudData.EndPosition = smogCloudConfig.RightPosition;
            }
            else
            {
                smogCloudData.StartPosition = smogCloudConfig.RightPosition;
                smogCloudData.EndPosition = smogCloudConfig.LeftPosition;
            }
            
            smogCloudController.SetPosition(smogCloudData.StartPosition);
        }

        private void StartWaiting()
        {
            smogCloudData.IsWaiting = true;
            smogCloudData.IsMoving = false;

            smogCloudData.WaitingTimer = 0;
            smogCloudData.WaitingDuration = Random.Range(smogCloudConfig.MinWaitingTime, smogCloudConfig.MaxWaitingTime);
            
            smogCloudController.SetPosition(smogCloudConfig.WaitingPosition);
        }

        private void UpdateWaiting()
        {
            if (!smogActivated) return;
            
            smogCloudData.WaitingTimer += Time.deltaTime;
            if (smogCloudData.WaitingTimer >= smogCloudData.WaitingDuration)
            {
                StartMoving();
            }
        }

        private void UpdateMoving()
        {
            smogCloudData.MovingTimer += Time.deltaTime;
            if (smogCloudData.MovingTimer >= smogCloudData.MovementDuration)
            {
                StartWaiting();
            }
            else
            {
                var lerpFactor = smogCloudData.MovingTimer / smogCloudData.MovementDuration;
                var nextPosition = Vector3.Lerp(smogCloudData.StartPosition, smogCloudData.EndPosition, lerpFactor);
                smogCloudController.SetPosition(nextPosition);
            }
        }
    }
}