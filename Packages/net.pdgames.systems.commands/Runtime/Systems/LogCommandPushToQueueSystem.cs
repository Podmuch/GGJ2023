using System.Collections.Generic;
using PDGames.EventBus;

namespace PDGames.Systems.Commands
{
    public sealed class LogCommandPushToQueueSystem : ReactiveSystem<CommandPushToQueueEvent>
    {
        public LogCommandPushToQueueSystem(IEventBus eventBus) : base(eventBus)
        {
        }

        protected override void Execute(List<CommandPushToQueueEvent> events)
        {
            for (int i = 0; i < events.Count; i++)
            {
                #if LOG_COMMAND_QUEUE
                Debug.Log("[CommandQueue] new command pushed: " + entities[i].Command.GetType().ToString() + " priority: " + entities[i].Priority.ToString() + " currentFrame=" + UnityEngine.Time.frameCount);
                #endif
            }
        }
    }
}
