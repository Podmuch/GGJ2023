using PDGames.DIContainer;
using PDGames.EventBus;
using PDGames.Systems;

namespace PDGames.UserInterface
{
    public sealed class UiHolderInitializationSystem : IInitializeSystem
    {
        private IDIContainer diContainer;
        
        public UiHolderInitializationSystem(IEventBus eventBus, IDIContainer diContainer)
        {
            this.diContainer = diContainer;
        }

        public void Initialize()
        {
            var uiHolder = new UiHolder();
            diContainer.Register(uiHolder);
        }
    }
}