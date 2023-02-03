using PDGames.DIContainer;
using PDGames.EventBus;
using PDGames.Systems.Loader;
using UnityEngine.SceneManagement;
using Utils;

namespace BoxColliders.Project
{
    public sealed class TransitionLoadMainSceneStep : LoaderStep
    {
        public TransitionLoadMainSceneStep(IEventBus eventBus, IDIContainer diContainer) : base(eventBus)
        {
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
            return DefinedLocaleKeys.GameInitialization;
        }

        public override void PerformStep()
        {
            var mainScene = SceneManager.GetSceneByName("MainScene");
            if (!mainScene.isLoaded) SceneManager.LoadScene("MainScene");
        }
    }
}