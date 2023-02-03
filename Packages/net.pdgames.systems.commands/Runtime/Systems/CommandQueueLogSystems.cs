using PDGames.DIContainer;
using PDGames.EventBus;

namespace PDGames.Systems.Commands
{
    public sealed class CommandQueueLogSystems : SystemsCascadeData
    {
        public CommandQueueLogSystems(IEventBus eventBus, IDIContainer diContainer)
        {
            Add(new LogCommandQueueChangeStateSystem(eventBus));
            Add(new LogCommandPushToQueueSystem(eventBus));
            Add(new LogCommandInProgressChangeSystem(eventBus));
            Add(new LogCommandQueueForceClearSystem(eventBus));
            Add(new LogCommandQueueExecutedSystem(eventBus));
        }
    }
}
