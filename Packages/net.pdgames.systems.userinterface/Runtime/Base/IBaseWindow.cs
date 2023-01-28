using PDGames.DIContainer;
using PDGames.EventBus;

namespace PDGames.UserInterface
{
    public interface IBaseWindow
    {
        bool IsVisible { get; }
        bool IsShowing { get; }
        bool IsHiding { get; }
        
        void Initialize(IEventBus eventBus, IDIContainer diContainer);
        void Deinitialize();
        
        void Show();
        void OnShown();
        
        void Hide();
        void OnHidden();
    }
}