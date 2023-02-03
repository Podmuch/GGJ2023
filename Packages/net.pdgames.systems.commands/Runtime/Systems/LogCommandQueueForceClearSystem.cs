using System.Collections.Generic;
using PDGames.EventBus;

namespace PDGames.Systems.Commands
{
    public sealed class LogCommandQueueForceClearSystem : ReactiveSystem<CommandQueueForceClearEvent>
    {
        public LogCommandQueueForceClearSystem(IEventBus eventBus) : base(eventBus)
        {
        }

        protected override void Execute(List<CommandQueueForceClearEvent> events)
        {
            for (int i = 0; i < events.Count; i++)
            {
                #if LOG_COMMAND_QUEUE
                Debug.Log("[CommandQueue] queue forced to clear!" + " currentFrame=" + UnityEngine.Time.frameCount);
                #endif
            }
        }
    }
}
