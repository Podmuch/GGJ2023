using System.Collections.Generic;
using PDGames.EventBus;

namespace PDGames.Systems.Commands
{
    public sealed class CommandQueue
    {
        public Dictionary<CommandPriority, Queue<AbstractCommand>> Queue;
        public AbstractCommand CommandInProgress 
        { 
            get { return commandInProgress; } 
            set 
            {
                commandInProgress = value;
                if (eventBus != null) eventBus.Fire<CommandInProgressChangedEvent>(new CommandInProgressChangedEvent() { Command = commandInProgress });
            } 
        }
        public bool IsOpened
        {
            get { return isOpened; }
            set
            {
                isOpened = value;
                if (eventBus != null) eventBus.Fire<CommandQueueChangeStateEvent>(new CommandQueueChangeStateEvent() { ShouldOpen = isOpened });
            }
        }

        private IEventBus eventBus;
        private AbstractCommand commandInProgress = null;
        private bool isOpened = false;

        public CommandQueue(IEventBus eventBus)
        {
            this.eventBus = eventBus;
            Queue = new Dictionary<CommandPriority, Queue<AbstractCommand>>(new CommandPriorityComparer());
        }

        public AbstractCommand GetCommand()
        {
            if(Queue.ContainsKey(CommandPriority.Critical) &&
               Queue[CommandPriority.Critical].Count > 0)
            {
                return Queue[CommandPriority.Critical].Dequeue();
            }
            else if(Queue.ContainsKey(CommandPriority.High) &&
               Queue[CommandPriority.High].Count > 0)
            {
                return Queue[CommandPriority.High].Dequeue();
            }
            else if (Queue.ContainsKey(CommandPriority.Normal) &&
               Queue[CommandPriority.Normal].Count > 0)
            {
                return Queue[CommandPriority.Normal].Dequeue();
            }
            else if (Queue.ContainsKey(CommandPriority.Low) &&
               Queue[CommandPriority.Low].Count > 0)
            {
                return Queue[CommandPriority.Low].Dequeue();
            }
            return null;
        }

        public bool IsEmpty()
        {
            if (commandInProgress != null) return false;

            foreach (var priorityLevel in Queue)
            {
                if (priorityLevel.Value.Count > 0) return false;
            }
            
            return true;
        }

        public void TearDown()
        {
            eventBus = null;
            Queue.Clear();
            CommandInProgress = null;
            IsOpened = false;
        }
    }
}
