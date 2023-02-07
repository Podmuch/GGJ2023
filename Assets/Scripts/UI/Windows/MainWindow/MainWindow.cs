using BoxColliders.Project;
using PDGames.UserInterface;
using UnityEngine;

namespace BoxColliders.Windows
{
    public sealed class MainWindow : BaseWindow<MainWindowView>
    {
        #region MONO BEHAVIOUR

        private void Update()
        {
            if (!IsVisible) return;
            
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("Fire3"))
            {
                OnPlayPressed();
            }
        }
        
        #endregion
        
        #region INPUT HANDLING
        
        public void OnPlayPressed()
        {
            eventBus.Fire<UiStartGameRequestEvent>();
        }
        
        #endregion
    }
}