using System.Collections.Generic;
using PDGames.DIContainer;
using PDGames.EventBus;
using PDGames.Systems;
using PDGames.Systems.Loader;

namespace BoxColliders.Game
{
    public sealed class GameStartGameplaySystem : ReactiveSystem<LoaderFinishedEvent>, IInitializeSystem
    {
        [DIInject] 
        private GameplayStateData stateData = default;
        
        private IDIContainer diContainer;
        private object diContext;
        
        public GameStartGameplaySystem(IEventBus eventBus, IDIContainer diContainer, object diContext) : base(eventBus)
        {
            this.diContainer = diContainer;
            this.diContext = diContext;
        }

        public void Initialize()
        {
            diContainer.Fetch(this, diContext);
        }

        protected override void Execute(List<LoaderFinishedEvent> entities)
        {
            stateData.IsStarted = true;
        }
    }
}