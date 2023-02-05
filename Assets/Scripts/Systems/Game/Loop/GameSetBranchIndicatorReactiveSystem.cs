using System.Collections.Generic;
using BoxColliders.Project;
using PDGames.DIContainer;
using PDGames.EventBus;
using PDGames.Systems;

namespace BoxColliders.Game
{
    public sealed class GameSetBranchIndicatorReactiveSystem : ReactiveSystem<SetBranchIndicatorEvent>, IInitializeSystem
    {
        [DIInject] 
        private GameplayStateData stateData = default;
        [DIInject] 
        private BranchIndicator branchIndicator = default;
        [DIInject] 
        private GameBranchesList branchesList = default;

        private IDIContainer diContainer;
        private object diContext;
        
        public GameSetBranchIndicatorReactiveSystem(IEventBus eventBus, IDIContainer diContainer, object diContext) : base(eventBus)
        {
            this.diContainer = diContainer;
            this.diContext = diContext;
        }

        public void Initialize()
        {
            diContainer.Fetch(this, diContext);
            SetPosition(0);
        }
        

        protected override void Execute(List<SetBranchIndicatorEvent> events)
        {
            var branchIndex = events[0].index;
            SetPosition(branchIndex);
        }

        private void SetPosition(int branchIndex)
        {
            if (branchesList.Branches.Count > 0)
            {
                var currentBranch = branchesList.Branches[branchIndex];
                currentBranch.HighlightStatIcon();
                //var currentBranchPosition = currentBranch.indicatorParent.transform.position;
                branchIndicator.SetPosition(currentBranch.indicatorParent.transform);
            }
        }
    }
}