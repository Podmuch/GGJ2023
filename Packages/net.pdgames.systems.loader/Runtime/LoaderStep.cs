using System;
using PDGames.EventBus;

namespace PDGames.Systems.Loader
{
    [Serializable]
    public abstract class LoaderStep
    {
        protected IEventBus eventBus;

        private bool isFinished = false;
        public bool IsFinished 
        {
            get
            {
                return isFinished;
            }
        }

        public LoaderStep(IEventBus eventBus)
        {
            this.eventBus = eventBus;
        }

        public abstract bool IsReady();
        public abstract float GetProgress();
        public abstract string GetDescription();

        public abstract void PerformStep();

        public virtual void FinishStep()
        {
            isFinished = true;
            eventBus.Fire<LoaderStepFinishedEvent>(new LoaderStepFinishedEvent());
        }
    }
}