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
        private GameResourcesConfig resourcesConfig = default;
        [DIInject] 
        private AudioController audioController = default;
        
        [SerializeField] 
        private List<SlotData> branchSlots;
        [SerializeField] 
        private SpriteRenderer stateIcon;
        [SerializeField] 
        public Transform indicatorParent;
        [SerializeField] 
        private Animator animator;
        
        [DIInject]
        private GameplayConfig gameplayConfig;
        
        private GameBranchStateData stateData = new GameBranchStateData();
        
        private IEventBus eventBus;
        private IDIContainer diContainer;
        private object diContext;
        
        private int stateEnumLength;
        
        public void Initialize(IEventBus eventBus, IDIContainer diContainer, object diContext, bool newBranch = false)
        {
            this.eventBus = eventBus;
            this.diContainer = diContainer;
            this.diContext = diContext;
            
            diContainer.Fetch(this, diContext);
            stateEnumLength = Enum.GetNames(typeof(BranchState)).Length;
            var randomState = (BranchState)Random.Range(0, stateEnumLength);

            SetData(randomState);
            stateData.isTakingAir = true;
            if (!newBranch) {ForceAnimationState("Idle");}
            else StartGrowAnimation();
        }

        public void ForceAnimationState(string animName)
        {
            if (animator == null) animator = GetComponent<Animator>();
            animator.Play(animName);
        }

        public void StartGrowAnimation()
        {
            if (animator == null) animator = GetComponent<Animator>();
            animator.SetTrigger("Grow");
        }

        public void SetNextState()
        {
            var currentState = (int)stateData.State;
            var nextState = currentState + 1;
            if (nextState >= stateEnumLength)
                nextState = 0;
            SetData((BranchState) nextState);

            audioController.PlayAudio(GetAudioOfState((BranchState) nextState));
        }
        
        public void SetData(BranchState branchState)
        {
            stateIcon.gameObject.SetActive(true);
            stateData.State = branchState;
            stateIcon.sprite = resourcesConfig.GetStateIcon(stateData.State);
            stateIcon.transform.rotation = Quaternion.identity;
        }
        
        public void DisableStateIcon()
        {
            stateIcon.gameObject.SetActive(false);
        }

        public Transform GetStatsIconTransform()
        {
            return stateIcon.transform;
        }

        public void HighlightStatIcon()
        {
            stateIcon.sortingOrder = 22;
            stateIcon.transform.localScale = Vector3.one * 1.5f;
        }

        public void UnhighlightStatIcon()
        {
            stateIcon.sortingOrder = 20;
            stateIcon.transform.localScale = Vector3.one;
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

        public string GetAudioOfState(BranchState branchState)
        {
            switch (branchState)
            {
                case BranchState.InActive:
                    return DefinedAudioKeys.modeOff;
                case BranchState.Water:
                    return DefinedAudioKeys.modeWater;
                case BranchState.Sun:
                    return DefinedAudioKeys.modeSun;
                case BranchState.Air:
                    return DefinedAudioKeys.modeAir;
            }

            return null;
        }
    }
}