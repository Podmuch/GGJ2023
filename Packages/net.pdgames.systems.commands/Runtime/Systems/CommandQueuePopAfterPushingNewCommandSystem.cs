﻿using System.Collections.Generic;
using PDGames.DIContainer;
using PDGames.EventBus;

namespace PDGames.Systems.Commands
{
    public sealed class CommandQueuePopAfterPushingNewCommandSystem : ReactiveSystem<CommandPushToQueueEvent>, IInitializeSystem
    {
        [DIInject]
        private CommandQueue commandQueue = default;

        private IDIContainer diContainer;
        
        public CommandQueuePopAfterPushingNewCommandSystem(IEventBus eventBus, IDIContainer diContainer) : base(eventBus)
        {
            this.diContainer = diContainer;
        }

        public void Initialize()
        {
            diContainer.Fetch(this);
        }

        protected override void Execute(List<CommandPushToQueueEvent> events)
        {
            if (commandQueue.CommandInProgress == null && commandQueue.IsOpened)
            {
                PerformCommand(commandQueue.GetCommand());
            }
        }

        private void PerformCommand(AbstractCommand command)
        {
            if (command != null)
            {
                command.PerformCommand();
                commandQueue.CommandInProgress = command;
            }
        }
    }
}
