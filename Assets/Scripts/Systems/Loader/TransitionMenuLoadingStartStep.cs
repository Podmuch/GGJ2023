using PDGames.DIContainer;
using PDGames.EventBus;
using PDGames.Systems.Loader;
using PDGames.UserInterface;
using BoxColliders.Windows;
using StemSystem;
using Utils;

namespace BoxColliders.Project
{
    public sealed class TransitionMenuLoadingStartStep : LoaderStep
    {
        private IDIContainer diContainer;

        public TransitionMenuLoadingStartStep(IEventBus eventBus, IDIContainer diContainer) : base(eventBus)
        {
            this.diContainer = diContainer;
        }

        public override bool IsReady()
        {
            return true;
        }

        public override float GetProgress()
        {
            return 1.0f;
        }
        
        public override string GetDescription()
        {
            return DefinedLocaleKeys.LoadingMenu;
        }

        public override void PerformStep()
        {
            Stem.UI.HideWindow<GameplayWindow>();
        }
    }
}