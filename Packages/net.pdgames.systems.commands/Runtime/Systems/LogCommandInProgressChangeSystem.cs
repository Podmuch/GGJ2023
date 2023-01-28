using System.Collections.Generic;
using PDGames.EventBus;

namespace PDGames.Systems.Commands
{
    public sealed class LogCommandInProgressChangeSystem : ReactiveSystem<CommandInProgressChangedEvent>
    {
        public LogCommandInProgressChangeSystem(IEventBus eventBus) : base(eventBus)
        {
        }

        protected override void Execute(List<CommandInProgressChangedEvent> events)
        {
            for (int i = 0; i < events.Count; i++)
            {
                var command = events[i].Command != null ? events[i].Command.GetType().ToString() : "NULL";

                #if LOG_COMMAND_QUEUE
                Debug.Log("[CommandQueue] command in progress changed: " + command + " currentFrame=" + UnityEngine.Time.frameCount);
                #endif
                events[i].IsDestroyed = true;
            }
        }
    }
}
