using PDGames.DIContainer;
using PDGames.EventBus;
using PDGames.Systems;
using UnityEngine;

namespace BoxColliders.Project
{
    public sealed class ProjectSystemsCascadeController : SystemsCascadeSingleton<ProjectSystemsCascadeController>
    {
        public override void Initialize()
        {
            if (!IsInitialized)
            {
                var container = GetComponentInChildren<ProjectDIContainer>();
                container.Initialize();
                
                var eventBus = GetComponentInChildren<ProjectEventBus>();
                eventBus.Initialize();
                container.Register(eventBus);
                container.Register(eventBus.GetCollector());
                
                base.Initialize();
            }
        }

        public override void Deinitialize()
        {
            if (IsInitialized)
            {
                var eventBus = ProjectEventBus.Instance;
                var container = ProjectDIContainer.Instance;
                GameObject.Destroy((eventBus as ProjectEventBus)?.gameObject);
                GameObject.Destroy((container as ProjectDIContainer)?.gameObject);
                base.Deinitialize();
            }
        }

        protected override SystemsCascadeData CreateSystems(IEventBus eventBus, IDIContainer diContainer)
        {
            return new ProjectSystemsCascadeData(eventBus, diContainer);
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