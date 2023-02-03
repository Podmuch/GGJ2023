using PDGames.DIContainer;
using PDGames.EventBus;
using PDGames.Systems;

namespace PDGames.UserInterface
{
    public sealed class UiManagementSystems : SystemsCascadeData
    {
        public UiManagementSystems(IEventBus eventBus, IDIContainer diContainer)
        {
            Add(new UiConfigInitializationSystem(eventBus, diContainer));
            Add(new UiHolderInitializationSystem(eventBus, diContainer));
            
            Add(new UiCanvasInitializationSystem(eventBus, diContainer));

            Add(new UiHideWindowSystem(eventBus, diContainer));
            Add(new UiShowWindowSystem(eventBus, diContainer));
        }
    }
}