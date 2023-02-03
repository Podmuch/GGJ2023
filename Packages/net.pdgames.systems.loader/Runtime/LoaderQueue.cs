using System.Collections.Generic;
using PDGames.EventBus;

namespace PDGames.Systems.Loader
{
    public sealed class LoaderQueue
    {
        public List<LoaderStep> Steps;

        public LoaderStep StepInProgress
        {
            get { return stepInProgress; } 
            set 
            {
                stepInProgress = value;
                if (eventBus != null) eventBus.Fire<LoaderStepInProgressChangedEvent>(new LoaderStepInProgressChangedEvent() { Step = stepInProgress });
            } 
        }

        public bool IsStarted
        {
            get { return isStarted; }
            set
            {
                if (isStarted != value)
                {
                    isStarted = value;
                    if (eventBus != null) eventBus.Fire<LoaderQueueStateEvent>(new LoaderQueueStateEvent() { ShouldStart = isStarted });
                }
            }
        }

        private IEventBus eventBus;
        private LoaderStep stepInProgress = null;
        private bool isStarted = false;
        
        public LoaderQueue(IEventBus eventBus)
        {
            this.eventBus = eventBus;
            Steps = new List<LoaderStep>();
        }

        public void TearDown()
        {
            eventBus = null;
            Steps.Clear();
            StepInProgress = null;
            IsStarted = false;
        }
    }
}