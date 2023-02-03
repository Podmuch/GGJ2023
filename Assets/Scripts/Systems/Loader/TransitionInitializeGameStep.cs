using System.Collections;
using PDGames.DIContainer;
using PDGames.EventBus;
using BoxColliders.Game;
using PDGames.Systems.Loader;
using UnityEngine;
using Utils;

namespace BoxColliders.Project
{
    public sealed class TransitionInitializeGameStep : LoaderStep
    {
        [DIInject] 
        private GameplayContextsHolder contextsHolder = default;
        
        private GameSystemCascadeController gameSystems;
        private IDIContainer diContainer;

        private Coroutine waitFrameCoroutine;

        private bool isReady = false;
        
        public TransitionInitializeGameStep(IEventBus eventBus, IDIContainer diContainer) : base(eventBus)
        {
            this.diContainer = diContainer;
        }

        public override bool IsReady()
        {
            return isReady;
        }

        public override float GetProgress()
        {
            return isReady ? 1.0f : 0.0f;
        }
        
        public override string GetDescription()
        {
            return DefinedLocaleKeys.LoadingGameplay;
        }

        public override void PerformStep()
        {
            diContainer.Fetch(this);
            
            gameSystems = diContainer.GetReference<GameSystemCascadeController>(contextsHolder.GameContext);
            gameSystems.Initialize();

            waitFrameCoroutine = gameSystems.StartCoroutine(WaitFrame());
        }

        public override void FinishStep()
        {
            base.FinishStep();

            if (waitFrameCoroutine != null)
            {
                gameSystems.StopCoroutine(waitFrameCoroutine);
                waitFrameCoroutine = null;
            }
        }

        private IEnumerator WaitFrame()
        {
            yield return null;
            isReady = true;
        }
    }
}