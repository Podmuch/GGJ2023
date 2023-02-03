using PDGames.DIContainer;
using PDGames.EventBus;

namespace PDGames.Systems.Commands
{
    public sealed class CommandQueueSystems : SystemsCascadeData
    {
        public CommandQueueSystems(IEventBus eventBus, IDIContainer diContainer)
        {
            Add(new InitializeCommandQueueSystem(eventBus, diContainer));
            Add(new CommandQueueLogSystems(eventBus, diContainer));
            Add(new CommandQueueForceClearSystem(eventBus, diContainer));
            Add(new CommandQueuePushSystem(eventBus, diContainer));
            Add(new CommandQueueClearExecutedSystem(eventBus, diContainer));
            Add(new CommandQueuePopSystems(eventBus, diContainer));
            Add(new CommandQueueFinishSystem(diContainer));
            Add(new TearDownCommandQueueSystem(eventBus, diContainer));
        }
    }
}
