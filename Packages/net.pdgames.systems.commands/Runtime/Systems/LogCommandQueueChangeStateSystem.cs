using System.Collections.Generic;
using PDGames.EventBus;

namespace PDGames.Systems.Commands
{
    public sealed class LogCommandQueueChangeStateSystem : ReactiveSystem<CommandQueueChangeStateEvent>
    {
        public LogCommandQueueChangeStateSystem(IEventBus eventBus) : base(eventBus)
        {
        }

        protected override void Execute(List<CommandQueueChangeStateEvent> events)
        {
            for (int i = 0; i < events.Count; i++)
            {
                #if LOG_COMMAND_QUEUE
                Debug.Log("[CommandQueue] queue state changed to: " + (entities[i].ShouldOpen ? "OPENED" : "CLOSED") + " currentFrame=" + UnityEngine.Time.frameCount);
                #endif
                events[i].IsDestroyed = true;
            }
        }
    }
}