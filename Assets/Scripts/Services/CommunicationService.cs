// using Systems.Game;
// using Systems.Gameplay;
// using Systems.Project;
using UnityEngine;

namespace Services
{
    public sealed class CommunicationService : Service
    {
        
        public override void Initialize()
        {
            base.Initialize();
            Debug.Log("<color=green>[Services]</color> ComunnicationService has been Initialized");
        }

        public override void Deinitialize()
        {
            base.Deinitialize();
            Debug.Log("<color=green>[Services]</color> ComunnicationService has been Deinitialized");
        }

        // public void StartGameplay()
        // {
        //     var gameplaySystemsController = MonoBehaviour.FindObjectOfType<GameplaySystemCascadeController>();
        //     var contextsHolder = new GameplayContextsHolder();
        //     contextsHolder.GameplaySystemController = gameplaySystemsController;
        //
        //     ProjectDIContainer.Instance.Register(contextsHolder, null);
        //     gameplaySystemsController.Initialize();
        //     ProjectEventBus.Instance.Fire<StartGameplayEvent>();
        // }
        //
        // public void FinishGameplay()
        // {
        //     var gameplaySystemsController = MonoBehaviour.FindObjectOfType<GameplaySystemCascadeController>();
        //     ProjectEventBus.Instance.Fire<FinishGameEvent>();
        //     gameplaySystemsController.Deinitialize();
        // }
    }
}
