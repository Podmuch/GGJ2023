using BoxColliders.Project;
using PDGames.UserInterface;

namespace BoxColliders.Windows
{
    public sealed class GameplayWindow : BaseWindow<GameplayWindowView>
    {
        #region INPUT HANDLING

        public void OnContinuePressed()
        {
            eventBus.Fire<UiEndGameRequestEvent>();
        }

        #endregion
    }
}