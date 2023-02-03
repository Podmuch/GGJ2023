using System.Collections.Generic;
using PDGames.DIContainer;
using PDGames.EventBus;

namespace PDGames.Systems.Commands
{
    public sealed class CommandQueueForceClearSystem : ReactiveSystem<CommandQueueForceClearEvent>, IInitializeSystem
    {
        [DIInject]
        private CommandQueue commandQueue = default;

        private IDIContainer diContainer;
        
        public CommandQueueForceClearSystem(IEventBus eventBus, IDIContainer diContainer) : base(eventBus)
        {
            this.diContainer = diContainer;
        }

        public void Initialize()
        {
            diContainer.Fetch(this);
        }

        protected override void Execute(List<CommandQueueForceClearEvent> events)
        {
            ClearQueue();
            FinishCurrentCommand();
            for(int i = 0; i < events.Count; i++)
            {
                events[i].IsDestroyed = true;
            }
        }

        private void ClearQueue()
        {
            foreach(var queueEntry in commandQueue.Queue)
            {
                queueEntry.Value.Clear();
            }
        }

        private void FinishCurrentCommand()
        {
            if(commandQueue.CommandInProgress != null)
            {
                commandQueue.CommandInProgress.FinishCommand();
            }
        }
    }
}
