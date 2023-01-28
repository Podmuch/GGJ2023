using PDGames.DIContainer;

namespace PDGames.Systems.Commands
{
    public sealed class CommandQueueFinishSystem : IExecuteSystem, IInitializeSystem
    {
        [DIInject]
        private CommandQueue commandQueue = default;
        
        private IDIContainer diContainer;

        public CommandQueueFinishSystem(IDIContainer diContainer)
        {
            this.diContainer = diContainer;
        }
        public void Initialize()
        {
            diContainer.Fetch(this);
        }

        public void Execute()
        {
            if(commandQueue.CommandInProgress != null &&
               commandQueue.CommandInProgress.IsReady())
            {
                commandQueue.CommandInProgress.FinishCommand();
            }
        }
    }
}
