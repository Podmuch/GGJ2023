using UnityEngine;

namespace Services
{
    public sealed class GameplayDataService : Service
    {
        public bool IsGameStarted { get; private set; }

        public int NumberOfPlayer;

        public override void Initialize()
        {
            base.Initialize();
            NumberOfPlayer = 4;
            Debug.Log("<color=green>[Services]</color> GameDataService has been Initialized");
        }

        public void Deinitialize()
        {
            base.Deinitialize();
            Debug.Log("<color=green>[Services]</color> GameDataService has been Deinitialized");
        }

        
        
        public void StartGame()
        {
            //Start game logic
            Debug.Log("GameStarted from GameDataService");
            
            IsGameStarted = true;
        }
    }
}
