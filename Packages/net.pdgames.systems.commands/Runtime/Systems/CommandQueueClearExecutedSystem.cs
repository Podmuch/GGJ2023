using System.Collections.Generic;
using PDGames.DIContainer;
using PDGames.EventBus;

namespace PDGames.Systems.Commands
{
    public sealed class CommandQueueClearExecutedSystem : ReactiveSystem<CommandExecutedEvent>, IInitializeSystem
    {
        [DIInject]
        private CommandQueue commandQueue = default;

        private IDIContainer diContainer;

        public CommandQueueClearExecutedSystem(IEventBus eventBus, IDIContainer diContainer) : base(eventBus)
        {
            this.diContainer = diContainer;
        }

        public void Initialize()
        {
            diContainer.Fetch(this);
        }

        protected override void Execute(List<CommandExecutedEvent> events)
        {
            commandQueue.CommandInProgress = null;
            for (int i = 0; i < events.Count; i++)
            {
                events[i].IsDestroyed = true;
            }
        }
    }
}
