using BoxColliders.Windows;
using Controllers;
using PDGames.DIContainer;
using PDGames.EventBus;
using PDGames.Systems;
using StemSystem;
using UnityEngine;

namespace BoxColliders.Game
{
    public sealed class GamePlayerInputSystem : IInitializeSystem, IExecuteSystem
    {
        [DIInject] 
        private GameplayStateData stateData = default;
        [DIInject] 
        private PlayerController playerController = default;
        [DIInject] 
        private EnemyController enemyController = default;
        
        private GameplayInputData inputData = default;
        private FixedJoystick joystick;
        
        private IDIContainer diContainer;
        private object diContext;
        
        public GamePlayerInputSystem(IEventBus eventBus, IDIContainer diContainer, object diContext)
        {
            this.diContainer = diContainer;
            this.diContext = diContext;
        }

        public void Initialize()
        {
            diContainer.Fetch(this, diContext);
            
            inputData = new GameplayInputData();
            this.diContainer.Register(inputData, diContext);
            
            joystick = Stem.UI.GetWindow<GameplayWindow>().GetJoystick();
            
        }
        
        public void Execute()
        {
            inputData.JoystickX = joystick.Horizontal;
            inputData.JoystickY = joystick.Vertical;
            
            inputData.IsAttackPressed = Input.GetKey(KeyCode.Space);
        }
    }
}