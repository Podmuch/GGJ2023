using System;
using PDGames.DIContainer;
using PDGames.EventBus;
using UnityEngine;

namespace PDGames.UserInterface
{
    [Serializable]
    public abstract class BaseWindowView
    {
        [Header("Base Window View")] 
        [SerializeField]
        protected CanvasGroup canvasGroup = default;
        
        protected IEventBus eventBus;
        protected IDIContainer diContainer;
        
        public virtual void Initialize(IEventBus eventBus, IDIContainer diContainer)
        {
            this.eventBus = eventBus;
            this.diContainer = diContainer;
            
            this.diContainer.Fetch(this);
        }

        public virtual void Deinitialize()
        {
        }
        
        public virtual void WillShow()
        {
        }

        public virtual void OnShown()
        {
        }

        public virtual void WillHide()
        {
        }

        public virtual void OnHidden()
        {
        }

        public void SetAlpha(float alpha)
        {
            canvasGroup.alpha = alpha;
        }
    }
}