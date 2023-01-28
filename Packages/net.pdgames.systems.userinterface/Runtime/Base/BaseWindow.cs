using PDGames.DIContainer;
using PDGames.EventBus;
using UnityEngine;

namespace PDGames.UserInterface
{
    [RequireComponent(typeof(CanvasGroup))]
    public abstract class BaseWindow<T> : MonoBehaviour, IBaseWindow where T : BaseWindowView
    {
        [SerializeField]
        protected T view = default;
        
        public bool IsVisible { get; protected set; }
        public bool IsShowing { get; protected set; }
        public bool IsHiding { get; protected set; }

        protected IEventBus eventBus;
        protected IDIContainer diContainer;
        
        public virtual void Initialize(IEventBus eventBus, IDIContainer diContainer)
        {
            this.eventBus = eventBus;
            this.diContainer = diContainer;
            view.Initialize(eventBus, diContainer);
            HideInternal(true, true);
        }

        public virtual void Deinitialize()
        {
            view.Deinitialize();
        }

        public virtual void Show()
        {
            view.WillShow();
            IsShowing = true;
            ShowInternal();
        }

        public virtual void OnShown()
        {
            IsShowing = false;
            IsVisible = true;
            view.OnShown();
        }

        public virtual void Hide()
        {
            view.WillHide();
            IsHiding = true;
            HideInternal();
        }

        public virtual void OnHidden()
        {
            IsHiding = false;
            IsVisible = false;
            view.OnHidden();
        }

        private void ShowInternal(bool animate = false, bool skipEvents = false)
        {
            gameObject.SetActive(true);
            view.SetAlpha(1.0f);
            transform.SetAsLastSibling();
            
            if (!skipEvents) OnShown();
        }

        private void HideInternal(bool animate = false, bool skipEvents = false)
        {
            gameObject.SetActive(false);
            view.SetAlpha(0.0f);
            
            if (!skipEvents) OnHidden();
        }
    }
}