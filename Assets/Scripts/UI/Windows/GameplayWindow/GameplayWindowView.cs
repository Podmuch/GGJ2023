using System;
using BoxColliders.Configs;
using BoxColliders.Controls;
using BoxColliders.Game;
using PDGames.DIContainer;
using PDGames.UserInterface;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace BoxColliders.Windows
{
    [Serializable]
    public sealed class GameplayWindowView : BaseWindowView
    {
        [DIInject] 
        private GameplayConfig gameplayConfig;
        [DIInject]
        private GameTreeStateData treeStateData;
        [DIInject] 
        private GameBranchesList gameBranchesList;
        
        [SerializeField] 
        private SimpleProgressBarView waterProgressBar;
        [SerializeField] 
        private SimpleProgressBarView sunProgressBar;
        [SerializeField] 
        private SimpleProgressBarView airProgressBar;

        [Space] 
        [SerializeField] 
        private TextMeshProUGUI waterConversionValue;
        [SerializeField] 
        private TextMeshProUGUI sunConversionValue;
        [SerializeField] 
        private TextMeshProUGUI airConversionValue;
        [SerializeField] 
        private Image energyFulfillImage;
        
        [Space]
        [SerializeField] 
        private GameObject resultPanel;
        [SerializeField]
        private TextMeshProUGUI resultBestSizeValue;
        [SerializeField] 
        private TextMeshProUGUI resultCurrentSizeValue;

        [Space]
        [SerializeField]
        private TextMeshProUGUI BestSizeValue;
        [SerializeField] 
        private TextMeshProUGUI CurrentSizeValue;
        
        public override void WillShow()
        {
            base.WillShow();
            var gameplayContexts = diContainer.GetReference<GameplayContextsHolder>(null);
            diContainer.Fetch(this, gameplayContexts.GameContext, true);

            var waterBarIndicatorValue = Mathf.InverseLerp(0, gameplayConfig.MaxWaterCapacity, gameplayConfig.WaterToEnergyConversion);
            var sunIndicatorValue = Mathf.InverseLerp(0, gameplayConfig.MaxSunCapacity, gameplayConfig.SunToEnergyConversion);
            var airBarIndicatorValue = Mathf.InverseLerp(0, gameplayConfig.MaxAirCapacity, gameplayConfig.AirToEnergyConversion);
            
            waterProgressBar.SetNeededIndicatorValue(waterBarIndicatorValue);
            sunProgressBar.SetNeededIndicatorValue(sunIndicatorValue);
            airProgressBar.SetNeededIndicatorValue(airBarIndicatorValue);
            
            waterConversionValue.text = gameplayConfig.WaterToEnergyConversion + "x";
            sunConversionValue.text = gameplayConfig.SunToEnergyConversion + "x";
            airConversionValue.text = gameplayConfig.AirToEnergyConversion + "x";
            UpdateView();
            resultPanel.SetActive(false);
        }

        public override void OnHidden()
        {
            base.OnHidden();
            diContainer.Unfetch(this);
        }

        public void UpdateView()
        {
            waterProgressBar.SetBarValue(treeStateData.CurrentWater / gameplayConfig.MaxWaterCapacity);
            waterProgressBar.SetTextValue(treeStateData.CurrentWater);
            
            sunProgressBar.SetBarValue(treeStateData.CurrentSun / gameplayConfig.MaxSunCapacity);
            sunProgressBar.SetTextValue(treeStateData.CurrentSun);
            
            airProgressBar.SetBarValue(treeStateData.CurrentAir / gameplayConfig.MaxAirCapacity);
            airProgressBar.SetTextValue(treeStateData.CurrentAir);
            
            energyFulfillImage.fillAmount = Mathf.Clamp01((float)treeStateData.Energy / (float)gameplayConfig.EnergyForGrow);

            BestSizeValue.text = "Best Size: " + gameBranchesList.BestBranchesCount;
            CurrentSizeValue.text = "Current: " + gameBranchesList.Branches.Count;
        }

        public void ShowResultPanel()
        {
            resultPanel.SetActive(true);
            resultBestSizeValue.text = BestSizeValue.text;
            resultCurrentSizeValue.text = CurrentSizeValue.text;
        }
    }
}