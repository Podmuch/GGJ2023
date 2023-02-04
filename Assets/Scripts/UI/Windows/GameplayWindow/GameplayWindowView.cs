using System;
using BoxColliders.Configs;
using BoxColliders.Controls;
using BoxColliders.Game;
using PDGames.DIContainer;
using PDGames.UserInterface;
using UnityEngine;

namespace BoxColliders.Windows
{
    [Serializable]
    public sealed class GameplayWindowView : BaseWindowView
    {
        [DIInject] 
        private GameplayConfig gameplayConfig;
        [DIInject]
        private GameTreeStateData treeStateData;
        
        [SerializeField] 
        private SimpleProgressBarView waterProgressBar;
        [SerializeField] 
        private SimpleProgressBarView sunProgressBar;
        [SerializeField] 
        private SimpleProgressBarView airProgressBar;
        
        public override void WillShow()
        {
            base.WillShow();
            var gameplayContexts = diContainer.GetReference<GameplayContextsHolder>(null);
            diContainer.Fetch(this, gameplayContexts.GameContext, true);
        }

        public override void OnHidden()
        {
            base.OnHidden();
            diContainer.Unfetch(this);
        }

        public void UpdateView()
        {
            waterProgressBar.SetValue(treeStateData.CurrentWater / gameplayConfig.MaxWaterCapacity);
            sunProgressBar.SetValue(treeStateData.CurrentSun / gameplayConfig.MaxSunCapacity);
            airProgressBar.SetValue(treeStateData.CurrentAir / gameplayConfig.MaxAirCapacity);
        }
    }
}