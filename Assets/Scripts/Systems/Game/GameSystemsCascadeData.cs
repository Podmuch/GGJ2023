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
            Add(new GameInitializeTreeStateDataSystem(eventBus, diContainer, diContext));
            Add(new GameInitializeDayNightCycleDataSystem(eventBus, diContainer, diContext));
            Add(new GameInitializeGameplaySunDataSystem(eventBus, diContainer, diContext));
            Add(new GameInitializeRainCloudDataSystem(eventBus, diContainer, diContext));
            Add(new GameInitializeSmogCloudDataSystem(eventBus, diContainer, diContext));
            Add(new GameInitializeBranchIndicatorDataSystem(eventBus, diContainer, diContext));
            
            Add(new GameDayNightCycleSystem(eventBus, diContainer, diContext));
            
            Add(new GameInputSystem(eventBus, diContainer, diContext));
            
            Add(new GameSetBranchIndicatorReactiveSystem(eventBus, diContainer, diContext));
            
            Add(new GameStartGameplaySystem(eventBus, diContainer, diContext));
            
            Add(new GameWaterProductionSystem(eventBus, diContainer, diContext));
            Add(new GameAirProductionSystem(eventBus, diContainer, diContext));
            Add(new GameSunProductionSystem(eventBus, diContainer, diContext));
            Add(new GameHealthConsumptionSystem(eventBus, diContainer, diContext));
            Add(new GameSetNewSunDataReactSystem(eventBus, diContainer, diContext));
            Add(new GameSunSystem(eventBus, diContainer, diContext));
            Add(new GameRainCloudSystem(eventBus, diContainer, diContext));
            Add(new GameSmogCloudSystem(eventBus, diContainer, diContext));
            Add(new GameEnergyProductionSystem(eventBus, diContainer, diContext));
            Add(new GameTreeGrowSystem(eventBus, diContainer, diContext));

            Add(new GameClearDiContainerSystem(eventBus, diContainer, diContext));
        }
    }
}