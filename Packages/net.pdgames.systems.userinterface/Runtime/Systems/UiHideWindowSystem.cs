using System.Collections.Generic;
using PDGames.DIContainer;
using PDGames.EventBus;
using PDGames.Systems;

namespace PDGames.UserInterface
{
    public sealed class UiHideWindowSystem : ReactiveSystem<UiHideWindowEvent>, IInitializeSystem
    {
        [DIInject]
        private UiHolder uiHolder = default;
        
        private IDIContainer diContainer;
        
        public UiHideWindowSystem(IEventBus eventBus, IDIContainer diContainer) : base(eventBus)
        {
            this.diContainer = diContainer;
        }

        public void Initialize()
        {
            diContainer.Fetch(this);
        }
        
        protected override void Execute(List<UiHideWindowEvent> events)
        {
            for (int i = 0; i < events.Count; i++)
            {
                string windowId = !string.IsNullOrEmpty(events[i].Id) ? events[i].Id : events[i].Type.Name;
                HideWindow(windowId);
                events[i].IsDestroyed = true;
            }
        }
        
        private void HideWindow(string windowId)
        {
            var windowToHide = uiHolder.GetWindow(windowId);
            if (windowToHide != null) windowToHide.Hide();
        }
    }
}