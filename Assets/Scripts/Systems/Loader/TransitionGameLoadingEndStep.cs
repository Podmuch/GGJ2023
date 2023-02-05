using PDGames.DIContainer;
using PDGames.EventBus;
using PDGames.Systems.Loader;
using PDGames.UserInterface;
using BoxColliders.Windows;
using Utils;

namespace BoxColliders.Project
{
    public sealed class TransitionGameLoadingEndStep : LoaderStep
    {
        [DIInject] 
        private MainCameraController mainCameraController;
        private IDIContainer diContainer;

        public TransitionGameLoadingEndStep(IEventBus eventBus, IDIContainer diContainer) : base(eventBus)
        {
            diContainer.Fetch(this);
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
            eventBus.Fire(new UiShowWindowEvent() { Type = typeof(GameplayWindow) });
            
            mainCameraController.Animator.SetTrigger(DefinedStrings.StartIntro);
        }
    }
}