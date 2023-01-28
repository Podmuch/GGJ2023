using BoxColliders.Project;
using PDGames.UserInterface;

namespace BoxColliders.Windows
{
    public sealed class MainWindow : BaseWindow<MainWindowView>
    {
        #region INPUT HANDLING
        
        public void OnPlayPressed()
        {
            eventBus.Fire<UiStartGameRequestEvent>();
        }
        
        #endregion
    }
}