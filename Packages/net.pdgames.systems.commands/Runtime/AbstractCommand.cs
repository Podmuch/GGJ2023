using System;
using PDGames.EventBus;

namespace PDGames.Systems.Commands
{
    [Serializable]
    public abstract class AbstractCommand
    {
        protected IEventBus eventBus;
        
        public AbstractCommand(IEventBus eventBus)
        {
            this.eventBus = eventBus;
        }

        public abstract bool IsReady();
        public abstract void PerformCommand();

        public virtual void FinishCommand()
        {
            eventBus.Fire<CommandExecutedEvent>(new CommandExecutedEvent() { Command = this });
        }
    }
}
