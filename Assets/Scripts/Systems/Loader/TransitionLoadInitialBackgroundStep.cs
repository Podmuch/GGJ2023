using BoxColliders.Configs;
using PDGames.DIContainer;
using PDGames.EventBus;
using PDGames.Systems.Loader;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utils;

namespace BoxColliders.Project
{
    public sealed class TransitionLoadInitialBackgroundStep : LoaderStep
    {
        [DIInject] 
        private ArenasConfig arenasConfig = default;
        [DIInject] 
        private MainCameraController mainCamera = default;
        
        private IDIContainer diContainer;
        
        private AsyncOperation sceneLoadOperation;

        private string sceneToLoad;
        private bool mapLoaded = false;
        
        public TransitionLoadInitialBackgroundStep(IEventBus eventBus, IDIContainer diContainer) : base(eventBus)
        {
            this.diContainer = diContainer;
            mapLoaded = false;
        }

        public override bool IsReady()
        {
            return mapLoaded;
        }

        public override float GetProgress()
        {
            return sceneLoadOperation != null ? sceneLoadOperation.progress : 0.0f;
        }

        public override string GetDescription()
        {
            return DefinedLocaleKeys.InterfaceInitialization;
        }

        public override void PerformStep()
        {
            diContainer.Fetch(this);

            sceneToLoad = "FirstArena";
            sceneLoadOperation = SceneManager.LoadSceneAsync(sceneToLoad, LoadSceneMode.Additive);
            sceneLoadOperation.completed += SceneLoadOperationOnCompleted;
        }
        
        private void SceneLoadOperationOnCompleted(AsyncOperation obj)
        {
            SceneLoadCompleted();
        }

        private void SceneLoadCompleted()
        {
            mapLoaded = true;
            var backgroundScene = SceneManager.GetSceneByName(sceneToLoad);
            SceneManager.SetActiveScene(backgroundScene);

            var arenaData = arenasConfig.Arenas.Find((arena) => arena.ArenaName == sceneToLoad);
            var loadedArenaDataHolder = new LoadedArenaDataHolder()
            {
                Data = arenaData,
                LoadedScene = backgroundScene
            };
            diContainer.Unregister(typeof(LoadedArenaDataHolder));
            diContainer.Register(loadedArenaDataHolder);
            
            SetupCamera(arenaData);
        }

        private void SetupCamera(ArenaData arenaData)
        {
            if (arenaData != null)
            {
                mainCamera.transform.position = arenaData.CameraPosition;
                mainCamera.transform.rotation = Quaternion.Euler(arenaData.CameraRotation);
                mainCamera.MainCamera.farClipPlane = arenaData.CameraFarClipPlane;
            }
        }
    }
}