using BoxColliders.Project;
using PDGames.DIContainer;
using PDGames.EventBus;
using PDGames.Systems;
using UnityEngine;

namespace BoxColliders.Game
{
    public sealed class GameInputSystem : IInitializeSystem, IExecuteSystem
    {
        [DIInject] 
        private GameplayStateData stateData = default;
        [DIInject] 
        private GameplayBranchIndicatorData branchIndicatorData;
        [DIInject] 
        private GameBranchesList branchesList = default;

        private IDIContainer diContainer;
        private object diContext;
        
        public GameInputSystem(IEventBus eventBus, IDIContainer diContainer, object diContext)
        {
            this.diContainer = diContainer;
            this.diContext = diContext;
        }

        public void Initialize()
        {
            diContainer.Fetch(this, diContext);
        }

        public void Execute()
        {
            if (!stateData.IsStarted) return;

            var branchesLastIndex = branchesList.Branches.Count - 1;
            var currentBranch = branchesList.Branches[branchIndicatorData.CurrentBranchIndex];
            var madeMove = false;
            
            if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
            {
                branchIndicatorData.CurrentBranchIndex -= 1;
                
                if (branchIndicatorData.CurrentBranchIndex <= -1)
                    branchIndicatorData.CurrentBranchIndex = branchesLastIndex;

                madeMove = true;
            }
            else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
            {
                branchIndicatorData.CurrentBranchIndex += 1;
                
                if (branchIndicatorData.CurrentBranchIndex > branchesLastIndex)
                    branchIndicatorData.CurrentBranchIndex = 0;
                
                madeMove = true;
            }

            else if (Input.GetKeyDown(KeyCode.Space))
            {
                currentBranch.SetNextState();
            }
            
            if (!madeMove) return;

            currentBranch.UnhighlightStatIcon();
            ProjectEventBus.Instance.Fire<SetBranchIndicatorEvent>(new SetBranchIndicatorEvent()
            {
                index =  branchIndicatorData.CurrentBranchIndex
            });
        }
    }
}