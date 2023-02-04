using BoxColliders.Configs;
using PDGames.DIContainer;
using PDGames.EventBus;
using PDGames.Systems;
using Systems.Data;
using UnityEngine;
using Utils;

namespace BoxColliders.Game
{
    public sealed class GameRainCloudSystem : IInitializeSystem, IExecuteSystem
    {
        [DIInject] 
        private GameplayStateData stateData = default;
        [DIInject]
        private GameplayRainCloudData rainCloudData = default;
        [DIInject] 
        private RainCloudController rainCloudController = default;
        [DIInject] 
        private GameplayRainCloudConfig rainCloudConfig = default;
        [DIInject] 
        private AudioController audioController = default;

        private AudioSource rainAudio;
        private IEventBus evenBus;
        private IDIContainer diContainer;
        private object diContext;
        
        public GameRainCloudSystem(IEventBus eventBus, IDIContainer diContainer, object diContext)
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

            if (!rainCloudData.IsWaiting && !rainCloudData.IsMoving)
            {
                var startFromMoving = Random.Range(0.0f, 1.0f) > 0.5f;
                if (startFromMoving) StartMoving();
                    else StartWaiting();
            }
            else if (rainCloudData.IsWaiting)
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
            rainCloudData.IsWaiting = false;
            rainCloudData.IsMoving = true;

            rainCloudData.MovingTimer = 0;
            rainCloudData.MovementDuration = Random.Range(rainCloudConfig.MinMovingTime, rainCloudConfig.MaxMovingTime);
            
            rainAudio = audioController.PlayAudio(DefinedAudioKeys.enviroRain, 0f, true, rainCloudData.MovementDuration);

            bool startFromLeft = Random.Range(0.0f, 1.0f) > 0.5f;
            if (startFromLeft)
            {
                rainCloudData.StartPosition = rainCloudConfig.LeftPosition;
                rainCloudData.EndPosition = rainCloudConfig.RightPosition;
            }
            else
            {
                rainCloudData.StartPosition = rainCloudConfig.RightPosition;
                rainCloudData.EndPosition = rainCloudConfig.LeftPosition;
            }
            
            rainCloudController.SetPosition(rainCloudData.StartPosition);
        }

        private void StartWaiting()
        {
            rainCloudData.IsWaiting = true;
            rainCloudData.IsMoving = false;

            rainCloudData.WaitingTimer = 0;
            rainCloudData.WaitingDuration = Random.Range(rainCloudConfig.MinWaitingTime, rainCloudConfig.MaxWaitingTime);
            
            rainCloudController.SetPosition(rainCloudConfig.WaitingPosition);
            
            rainAudio?.Stop();
        }

        private void UpdateWaiting()
        {
            rainCloudData.WaitingTimer += Time.deltaTime;
            if (rainCloudData.WaitingTimer >= rainCloudData.WaitingDuration)
            {
                StartMoving();
            }
        }

        private void UpdateMoving()
        {
            rainCloudData.MovingTimer += Time.deltaTime;
            if (rainCloudData.MovingTimer >= rainCloudData.MovementDuration)
            {
                StartWaiting();
            }
            else
            {
                var lerpFactor = rainCloudData.MovingTimer / rainCloudData.MovementDuration;
                var nextPosition = Vector3.Lerp(rainCloudData.StartPosition, rainCloudData.EndPosition, lerpFactor);
                rainCloudController.SetPosition(nextPosition);
            }
        }
    }
}