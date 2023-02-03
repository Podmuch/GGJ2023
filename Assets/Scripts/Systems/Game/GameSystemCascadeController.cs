using PDGames.DIContainer;
using PDGames.EventBus;
using BoxColliders.Project;
using PDGames.Systems;

namespace BoxColliders.Game
{
    public sealed class GameSystemCascadeController : SystemsCascadeController
    {
        protected override SystemsCascadeData CreateSystems(IEventBus eventBus, IDIContainer diContainer)
        {
            return new GameSystemsCascadeData(eventBus, diContainer, this);
        }

        protected override IEventBus GetEventBus()
        {
            return ProjectEventBus.Instance;
        }

        protected override IDIContainer GetDiContainer()
        {
            return ProjectDIContainer.Instance;
        }
    }
}