using PDGames.DIContainer;
using PDGames.EventBus;

namespace PDGames.Systems.Commands
{
    public sealed class CommandQueuePopSystems : SystemsCascadeData
    {
        public CommandQueuePopSystems(IEventBus eventBus, IDIContainer diContainer)
        {
            Add(new CommandQueuePopAfterExecutedCommandSystem(eventBus, diContainer));
            Add(new CommandQueuePopAfterOpenningQueueSystem(eventBus, diContainer));
            Add(new CommandQueuePopAfterPushingNewCommandSystem(eventBus, diContainer));
        }
    }
}
