using PDGames.DIContainer;
using PDGames.EventBus;

namespace PDGames.Systems.Loader
{
    public sealed class InitializeLoaderQueueSystem : IInitializeSystem, ITearDownSystem
    {
        private LoaderQueue loaderQueue = null;
        
        private IEventBus eventBus;
        private IDIContainer diContainer;
            
        public InitializeLoaderQueueSystem(IEventBus eventBus, IDIContainer diContainer)
        {
            this.eventBus = eventBus;
            this.diContainer = diContainer;
        }
        public void Initialize()
        {
            loaderQueue = new LoaderQueue(eventBus);
            diContainer.Register(loaderQueue);
        }

        public void TearDown()
        {
            if (loaderQueue != null) loaderQueue.TearDown();
        }
    }
}
