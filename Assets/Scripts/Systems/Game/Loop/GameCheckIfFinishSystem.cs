using BoxColliders.Windows;
using PDGames.DIContainer;
using PDGames.EventBus;
using PDGames.Systems;
using PDGames.UserInterface;

namespace BoxColliders.Game
{
    public sealed class GameCheckIfFinishSystem : IInitializeSystem, IExecuteSystem
    {
        [DIInject] private GameplayStateData stateData = default;
        [DIInject] private GameBranchesList branchesList = default;
        [DIInject] private UiHolder uiHolder = default;

        private IDIContainer diContainer;
        private object diContext;
        private IEventBus eventBus;
        private bool resultShown = false;

        public GameCheckIfFinishSystem(IEventBus eventBus, IDIContainer diContainer, object diContext)
        {
            this.diContainer = diContainer;
            this.diContext = diContext;
            this.eventBus = eventBus;
        }

        public void Initialize()
        {
            diContainer.Fetch(this, diContext);
        }

        public void Execute()
        {
            if (stateData.IsStarted && branchesList.Branches.Count <= 0 && !resultShown)
            {
                var gameplayWindow = uiHolder.GetWindow<GameplayWindow>();
                gameplayWindow.ShowResultPanel();
                resultShown = true;
            }
        }
    }
}