using UnityEngine;
using PDGames.DIContainer;
using PDGames.EventBus;

namespace PDGames.Systems
{
    public abstract class SystemsCascadeController : MonoBehaviour
    {
        public enum UpdateType
        {
            Normal,
            Fixed,
            Late
        }

        [SerializeField]
        protected UpdateType updateType = UpdateType.Normal;
        [SerializeField] 
        protected bool initializeOnStart = true;

        protected SystemsCascadeData cascadeData;
        
        public bool IsInitialized { get; protected set; }

        #region MONO BEHAVIOUR

        protected virtual void Start()
        {
            // Suggested systems lifecycle:
            // systems.Initialize() on Start
            // systems.Execute() on Update
            // systems.Cleanup() on Update after systems.Execute()
            // systems.TearDown() on OnDestroy

            if (initializeOnStart)
            {
                Initialize(); 
            }
        }

        private void Update()
        {
            if (IsInitialized && updateType == UpdateType.Normal)
            {
                ExecuteCascade();
            }
        }
        
        private void FixedUpdate()
        {
            if (IsInitialized && updateType == UpdateType.Fixed)
            {
                ExecuteCascade();
            }
        }
        
        private void LateUpdate()
        {
            if (IsInitialized && updateType == UpdateType.Late)
            {
                ExecuteCascade();
            }
        }

        protected virtual void OnDestroy()
        {
            Deinitialize();
        }

        #endregion

        public virtual void Initialize()
        {
            if (!IsInitialized)
            {
                cascadeData = CreateSystems(GetEventBus(), GetDiContainer());
                for (int i = 0; i < cascadeData.InitializeSystems.Count; i++)
                {
                    cascadeData.InitializeSystems[i].Initialize();
                }

                IsInitialized = true;
            }
        }

        public virtual void Deinitialize()
        {
            if (IsInitialized)
            {
                for (int i = 0; i < cascadeData.TearDownSystems.Count; i++)
                {
                    cascadeData.TearDownSystems[i].TearDown();
                }

                cascadeData.Clear();
                cascadeData = null;
                IsInitialized = false;
            }
        }
        
        protected abstract SystemsCascadeData CreateSystems(IEventBus eventBus, IDIContainer diContainer);
        protected abstract IEventBus GetEventBus();
        protected abstract IDIContainer GetDiContainer();

        private void ExecuteCascade()
        {
            for (int i = 0; i < cascadeData.ExecuteSystems.Count; i++)
            {
                cascadeData.ExecuteSystems[i].Execute();
            }

            for (int i = 0; i < cascadeData.CleanupSystems.Count; i++)
            {
                cascadeData.CleanupSystems[i].Execute();
            }
        }
    }
}