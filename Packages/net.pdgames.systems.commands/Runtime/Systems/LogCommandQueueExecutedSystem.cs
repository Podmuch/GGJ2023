using System.Collections.Generic;
using PDGames.EventBus;

namespace PDGames.Systems.Commands
{
    public sealed class LogCommandQueueExecutedSystem : ReactiveSystem<CommandExecutedEvent>
    {
        public LogCommandQueueExecutedSystem(IEventBus eventBus) : base(eventBus)
        {
        }

        protected override void Execute(List<CommandExecutedEvent> events)
        {
            for (int i = 0; i < events.Count; i++)
            {
                #if LOG_COMMAND_QUEUE
                Debug.Log("[CommandQueue] command executed: " + entities[i].Command.GetType().ToString() + " currentFrame=" + UnityEngine.Time.frameCount);
                #endif  
            }
        }
    }
}
