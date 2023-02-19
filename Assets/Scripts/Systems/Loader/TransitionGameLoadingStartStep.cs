using PDGames.DIContainer;
using PDGames.EventBus;
using PDGames.Systems.Loader;
using PDGames.UserInterface;
using BoxColliders.Windows;
using StemSystem;
using Utils;

namespace BoxColliders.Project
{
    public sealed class TransitionGameLoadingStartStep : LoaderStep
    {
        private IDIContainer diContainer;

        public TransitionGameLoadingStartStep(IEventBus eventBus, IDIContainer diContainer) : base(eventBus)
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
            return DefinedLocaleKeys.LoadingGameplay;
        }

        public override void PerformStep()
        {
            Stem.UI.HideWindow<MainWindow>();;
        }
    }
}