using System;
using BoxColliders.Game;
using PDGames.UserInterface;
using UnityEngine;

namespace BoxColliders.Windows
{
    [Serializable]
    public sealed class GameplayWindowView : BaseWindowView
    {
        [SerializeField]
        private FixedJoystick joystick;
        
        public override void WillShow()
        {
            base.WillShow();
            var gameplayContexts = diContainer.GetReference<GameplayContextsHolder>(null);
            diContainer.Fetch(this, gameplayContexts.GameContext);
        }

        public override void OnHidden()
        {
            base.OnHidden();
            diContainer.Unfetch(this);
        }
        
        public FixedJoystick GetJoystick()
        {
            return joystick;
        }
    }
}