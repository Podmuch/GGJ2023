using BoxColliders.Project;
using PDGames.UserInterface;

namespace BoxColliders.Windows
{
    public sealed class GameplayWindow : BaseWindow<GameplayWindowView>
    {

        public FixedJoystick GetJoystick()
        {
            return view.GetJoystick();
        }
        
        #region INPUT HANDLING

        public void OnContinuePressed()
        {
            eventBus.Fire<UiEndGameRequestEvent>();
        }

        #endregion
    }
}