using System;
using System.Collections.Generic;
using BoxColliders.Configs;
using PDGames.DIContainer;
using PDGames.EventBus;
using UnityEngine;
using Utils;
using Random = UnityEngine.Random;

namespace BoxColliders.Game
{
    public sealed class GameBranchController : MonoBehaviour
    {
        [DIInject] 
        private GameResourcesConfig resourcesConfig;
        
        [SerializeField] 
        private List<SlotData> branchSlots;
        [SerializeField] 
        private SpriteRenderer stateIcon;
        
        [DIInject]
        private GameplayConfig gameplayConfig;
        
        [SerializeField]
        private GameBranchStateData stateData = new GameBranchStateData();

        private IEventBus eventBus;
        private IDIContainer diContainer;
        private object diContext;

        public void Initialize(IEventBus eventBus, IDIContainer diContainer, object diContext)
        {
            this.eventBus = eventBus;
            this.diContainer = diContainer;
            this.diContext = diContext;
            
            diContainer.Fetch(this, diContext);
            stateData.State = (BranchState)Random.Range(0, Enum.GetNames(typeof(BranchState)).Length);
            
            stateIcon.gameObject.SetActive(true);
            stateIcon.sprite = resourcesConfig.GetStateIcon(stateData.State);
            stateIcon.transform.rotation = Quaternion.identity;
        }

        public void DisableStateIcon()
        {
            stateIcon.gameObject.SetActive(false);
        }
        
        public bool CanProduceWater()
        {
            return stateData.State == BranchState.Water && stateData.IsTakingWater;
        }

        public float GetWaterProduction()
        {
            return gameplayConfig.BranchWaterProduction * Time.deltaTime;
        }

        public bool CanProduceAir()
        {
            return stateData.State == BranchState.Air && stateData.isTakingAir;
        }

        public float GetAirProduction()
        {
            return gameplayConfig.BranchAirProduction * Time.deltaTime;
        }

        public bool CanProduceSun()
        {
            return stateData.State == BranchState.Sun && stateData.IsTakingSun;
        }

        public float GetSunProduction()
        {
            return gameplayConfig.BranchSunProduction * Time.deltaTime;
        }
        
        public List<SlotData> GetBranchSlots()
        {
            return branchSlots;
        }
        
        void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag(DefinedStrings.Sun))
            {
                stateData.IsTakingSun = true;
            }
            
            if (collision.gameObject.CompareTag(DefinedStrings.Rain))
            {
                stateData.IsTakingWater = true;
            }
            
            if (collision.gameObject.CompareTag(DefinedStrings.Smog))
            {
                stateData.isTakingAir = false;
            }
        }
        
        void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag(DefinedStrings.Sun))
            {
                stateData.IsTakingSun = false;
            }
            
            if (collision.gameObject.CompareTag(DefinedStrings.Rain))
            {
                stateData.IsTakingWater = false;
            }
            
            if (collision.gameObject.CompareTag(DefinedStrings.Smog))
            {
                stateData.isTakingAir = true;
            }
        }
    }
}