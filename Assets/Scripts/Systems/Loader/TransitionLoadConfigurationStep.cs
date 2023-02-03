using PDGames.DIContainer;
using PDGames.EventBus;
using PDGames.Systems.Loader;
using Utils;

namespace BoxColliders.Project
{
    public sealed class TransitionLoadConfigurationStep : LoaderStep
    {
        private InitializeConfigurationEvent initializeConfigurationEvent;
        
        public TransitionLoadConfigurationStep(IEventBus eventBus, IDIContainer diContainer) : base(eventBus)
        {
        }

        public override bool IsReady()
        {
            return initializeConfigurationEvent != null && initializeConfigurationEvent.IsProcessed;
        }

        public override float GetProgress()
        {
            return initializeConfigurationEvent != null && initializeConfigurationEvent.IsProcessed ? 1.0f : 0.0f;
        }

        public override string GetDescription()
        {
            return DefinedLocaleKeys.GameInitialization;
        }

        public override void PerformStep()
        {
            initializeConfigurationEvent = new InitializeConfigurationEvent();
            eventBus.Fire(initializeConfigurationEvent);
        }

        public override void FinishStep()
        {
            base.FinishStep();
            initializeConfigurationEvent.IsDestroyed = true;
        }
    }
}