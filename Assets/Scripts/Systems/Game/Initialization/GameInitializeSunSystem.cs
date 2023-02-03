using System.Collections.Generic;
using PDGames.DIContainer;
using PDGames.EventBus;
using PDGames.Systems;
using PDGames.Systems.Loader;

namespace BoxColliders.Game
{
    public sealed class GameInitializeSunSystem : IInitializeSystem
    {
        [DIInject] 
        private GameplayStateData stateData = default;
        
        private IDIContainer diContainer;
        private object diContext;
        
        public GameInitializeSunSystem(IEventBus eventBus, IDIContainer diContainer, object diContext)
        {
            this.diContainer = diContainer;
            this.diContext = diContext;
        }

        public void Initialize()
        {
            diContainer.Fetch(this, diContext);
        }
    }
}