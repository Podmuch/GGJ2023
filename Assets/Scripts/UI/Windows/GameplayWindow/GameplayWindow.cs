using BoxColliders.Project;
using PDGames.UserInterface;

namespace BoxColliders.Windows
{
    public sealed class GameplayWindow : BaseWindow<GameplayWindowView>
    {
        #region MONO BEHAVIOUR

        private void Update()
        {
            view.UpdateView();
        }
        
        #endregion
        
        #region INPUT HANDLING

        public void OnContinuePressed()
        {
            eventBus.Fire<UiEndGameRequestEvent>();
        }

        #endregion
    }
}