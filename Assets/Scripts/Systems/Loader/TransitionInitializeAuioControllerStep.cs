using PDGames.DIContainer;
using PDGames.EventBus;
using PDGames.Systems.Loader;
using UnityEngine;
using Utils;

namespace BoxColliders.Project
{
    public sealed class TransitionInitializeAudioControllerStep : LoaderStep
    {
        [DIInject] 
        private AudioController audioController;
        private IDIContainer diContainer;

        public TransitionInitializeAudioControllerStep(IEventBus eventBus, IDIContainer diContainer) : base(eventBus)
        {
            this.diContainer = diContainer;
            diContainer.Fetch(this);
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
            return DefinedLocaleKeys.InterfaceInitialization;
        }

        public override void PerformStep()
        {
            var audioController = GameObject.FindObjectOfType<AudioController>();
            diContainer.Register(audioController);

            audioController.PlayAudio(DefinedAudioKeys.themeSong, 0f, true);
        }
    }
}