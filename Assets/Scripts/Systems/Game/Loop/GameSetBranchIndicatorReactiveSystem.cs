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
            var currentBranchPosition = branchesList.Branches[0].transform.position;
            branchIndicator.SetPosition(currentBranchPosition);
        }
        

        protected override void Execute(List<SetBranchIndicatorEvent> events)
        {
            var setBranchEvent = events[0];
            
            var currentBranchPosition = branchesList.Branches[setBranchEvent.index].transform.position;
            branchIndicator.SetPosition(currentBranchPosition);
        }
    }
}