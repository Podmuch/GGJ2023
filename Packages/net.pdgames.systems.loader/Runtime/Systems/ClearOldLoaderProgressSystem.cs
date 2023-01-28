using System.Collections.Generic;
using PDGames.EventBus;

namespace PDGames.Systems.Loader
{
    public sealed class ClearOldLoaderProgressSystem : ReactiveSystem<LoaderProgressEvent>
    {
        public ClearOldLoaderProgressSystem(IEventBus eventBus) : base(eventBus)
        {
        }
        
        protected override void Execute(List<LoaderProgressEvent> events)
        {
            for (int i = 0; i < events.Count; i++)
            {
                events[i].IsDestroyed = true;
            }
        }
    }
}