using System;
using System.Collections;
using PDGames.DIContainer;
using PDGames.EventBus;
using PDGames.Systems.Loader;
using UnityEngine;
using Utils;

namespace BoxColliders.Project
{
    public sealed class TransitionClearResourcesStep : LoaderStep
    {
        [DIInject]
        private MonoBehaviourHost monoBehaviourHost = default;
        
        private AsyncOperation unloadOperation = null;
        private Coroutine clearingCoroutine = null;
        
        private IDIContainer diContainer;

        private bool clearingFinished = false;
        
        public TransitionClearResourcesStep(IEventBus eventBus, IDIContainer diContainer) : base(eventBus)
        {
            this.diContainer = diContainer;
        }

        public override bool IsReady()
        {
            return clearingFinished;
        }

        public override float GetProgress()
        {
            return clearingFinished ? 1.0f : 
                unloadOperation != null ? unloadOperation.progress : 0.0f;
        }
        
        public override string GetDescription()
        {
            return DefinedLocaleKeys.LoadingMenu;
        }

        public override void PerformStep()
        {
            diContainer.Fetch(this);
            clearingCoroutine = monoBehaviourHost.StartCoroutine(ClearingCoroutine());
        }
        
        public override void FinishStep()
        {
            if (clearingCoroutine != null)
            {
                monoBehaviourHost.StopCoroutine(clearingCoroutine);
                clearingCoroutine = null;
            }
            unloadOperation = null;
            base.FinishStep();
        }
        
        private IEnumerator ClearingCoroutine()
        {
            unloadOperation = Resources.UnloadUnusedAssets();
            yield return unloadOperation;
            yield return null;
            GC.Collect();
            yield return null;
            clearingFinished = true;
        }
    }
}