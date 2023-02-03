using System.Collections.Generic;
using PDGames.EventBus;

namespace PDGames.Systems.Loader
{
    public sealed class ClearLoaderStepInProgressChangedEventSystem : ReactiveSystem<LoaderStepInProgressChangedEvent>
    {
        public ClearLoaderStepInProgressChangedEventSystem(IEventBus eventBus) : base(eventBus)
        {
        }
        
        protected override void Execute(List<LoaderStepInProgressChangedEvent> events)
        {
            for (int i = 0; i < events.Count; i++)
            {
                events[i].IsDestroyed = true;
            }
        }
    }
}