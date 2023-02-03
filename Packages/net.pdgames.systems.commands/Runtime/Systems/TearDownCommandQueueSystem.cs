using PDGames.DIContainer;
using PDGames.EventBus;

namespace PDGames.Systems.Commands
{
    public sealed class TearDownCommandQueueSystem : ITearDownSystem
    {
        [DIInject]
        private CommandQueue commandQueue = default;

        private IEventBus eventBus;
        private IDIContainer diContainer;
        
        public TearDownCommandQueueSystem(IEventBus eventBus, IDIContainer diContainer)
        {
            this.eventBus = eventBus;
            this.diContainer = diContainer;
        }
        public void TearDown()
        {
            diContainer.Fetch(this);
            if (commandQueue != null)
            {
                foreach(var queueEntry in commandQueue.Queue)
                {
                    queueEntry.Value.Clear();
                }
                if(commandQueue.CommandInProgress != null)
                {
                    commandQueue.CommandInProgress.FinishCommand();
                }
            }
        }
    }
}