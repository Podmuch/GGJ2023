using PDGames.DIContainer;
using PDGames.EventBus;
using PDGames.Systems;

namespace BoxColliders.Game
{
    public sealed class GameSystemsCascadeData : SystemsCascadeData
    {
        public GameSystemsCascadeData(IEventBus eventBus, IDIContainer diContainer, object diContext)
        {
            Add(new GameInitializeGameplayStateDataSystem(eventBus, diContainer, diContext));
            Add(new GamePlayerInputSystem(eventBus, diContainer, diContext));
            Add(new GamePlayerMoveSystem(eventBus, diContainer, diContext));

            Add(new GameStartGameplaySystem(eventBus, diContainer, diContext));
            
            Add(new GameClearDiContainerSystem(eventBus, diContainer, diContext));
        }
    }
}