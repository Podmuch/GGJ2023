using System.Collections.Generic;
using PDGames.DIContainer;
using PDGames.EventBus;

namespace PDGames.Systems.Commands
{
    public sealed class CommandQueuePushSystem : ReactiveSystem<CommandPushToQueueEvent>, IInitializeSystem
    {
        [DIInject]
        private CommandQueue commandQueue = default;

        private IDIContainer diContainer;
        
        public CommandQueuePushSystem(IEventBus eventBus, IDIContainer diContainer) : base(eventBus)
        {
            this.diContainer = diContainer;
        }

        public void Initialize()
        {
            diContainer.Fetch(this);
        }

        protected override void Execute(List<CommandPushToQueueEvent> events)
        {
            for (int i = 0; i < events.Count; i++)
            {
                AddToCommandQueue(events[i].Priority, events[i].Command);
                ClearForCriticalCommandsIfNeeded();
                ClearLowPriorityCommandsIfNeeded();
                events[i].IsDestroyed = true;
            }
        }

        private void AddToCommandQueue(CommandPriority type, AbstractCommand command)
        {
            if(!commandQueue.Queue.ContainsKey(type))
            {
                commandQueue.Queue.Add(type, new Queue<AbstractCommand>());
            }
            commandQueue.Queue[type].Enqueue(command);
        }

        private void ClearForCriticalCommandsIfNeeded()
        {
            bool shouldClear = commandQueue.Queue.ContainsKey(CommandPriority.Critical) &&
                               commandQueue.Queue[CommandPriority.Critical].Count > 0;
            if(shouldClear)
            {
                foreach(var queueEntry in commandQueue.Queue)
                {
                    if(queueEntry.Key != CommandPriority.Critical)
                    {
                        queueEntry.Value.Clear();
                    }
                }
            }
        }

        private void ClearLowPriorityCommandsIfNeeded()
        {
            bool shouldClear = commandQueue.Queue.Count > 1;

            if (shouldClear)
            {
                foreach (var queueEntry in commandQueue.Queue)
                {
                    if (queueEntry.Key == CommandPriority.Low)
                    {
                        queueEntry.Value.Clear();
                    }
                }
            }
        }
    }
}
