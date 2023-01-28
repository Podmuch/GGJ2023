using PDGames.DIContainer;
using PDGames.EventBus;
using BoxColliders.Game;
using PDGames.Systems.Loader;
using Utils;

namespace BoxColliders.Project
{
    public sealed class TransitionLoadGameControllersStep : LoaderStep
    {
        [DIInject] 
        private GameplayObjectsPool objectsPool = default;
        
        private IDIContainer diContainer;

        private bool isReady = false;
        
        public TransitionLoadGameControllersStep(IEventBus eventBus, IDIContainer diContainer) : base(eventBus)
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
            var diContext = diContainer.GetReference<GameplayContextsHolder>(null).GameContext;
            diContainer.Fetch(this, diContext);
            
            isReady = true;
        }
    }
}