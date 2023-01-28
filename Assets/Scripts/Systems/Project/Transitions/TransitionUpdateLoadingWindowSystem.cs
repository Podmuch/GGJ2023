using System.Collections.Generic;
using PDGames.DIContainer;
using PDGames.EventBus;
using PDGames.Systems;
using PDGames.Systems.Loader;
using PDGames.UserInterface;
using BoxColliders.Windows;

namespace BoxColliders.Project
{
    public sealed class TransitionUpdateLoadingWindowSystem : ReactiveSystem<LoaderProgressEvent>, IInitializeSystem
    {
        [DIInject] 
        private UiHolder uiHolder = default;
        
        private IDIContainer diContainer;

        public TransitionUpdateLoadingWindowSystem(IEventBus eventBus, IDIContainer diContainer) : base(eventBus)
        {
            this.diContainer = diContainer;
        }

        public void Initialize()
        {
            diContainer.Fetch(this);
        }

        protected override void Execute(List<LoaderProgressEvent> entities)
        {
            var loadingWindow = uiHolder.GetWindow<LoadingWindow>();
            if (loadingWindow != null && (loadingWindow.IsVisible || loadingWindow.IsShowing))
            {
                for (int i = 0; i < entities.Count; i++)
                {
                    loadingWindow.UpdateView(entities[i].Progress, entities[i].Description, 
                        entities[i].CurrentStepIndex.ToString(), entities[i].TotalSteps.ToString());
                }
            }
        }
    }
}