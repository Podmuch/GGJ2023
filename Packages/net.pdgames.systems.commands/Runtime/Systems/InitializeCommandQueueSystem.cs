using PDGames.DIContainer;
using PDGames.EventBus;

namespace PDGames.Systems.Commands
{
    public sealed class InitializeCommandQueueSystem : IInitializeSystem, ITearDownSystem
    {
        private CommandQueue commandQueue = null;
        
        private IEventBus eventBus;
        private IDIContainer diContainer;
        
        public InitializeCommandQueueSystem(IEventBus eventBus, IDIContainer diContainer)
        {
            this.eventBus = eventBus;
            this.diContainer = diContainer;
        }
        public void Initialize()
        {
            commandQueue = new CommandQueue(eventBus);
            diContainer.Register(commandQueue);
        }

        public void TearDown()
        {
            if (commandQueue != null) commandQueue.TearDown();
        }
    }
}
