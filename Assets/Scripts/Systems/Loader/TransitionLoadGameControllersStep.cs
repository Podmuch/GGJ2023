using System.Collections.Generic;
using BoxColliders.Configs;
using PDGames.DIContainer;
using PDGames.EventBus;
using BoxColliders.Game;
using PDGames.Systems.Loader;
using UnityEngine;
using Utils;

namespace BoxColliders.Project
{
    public sealed class TransitionLoadGameControllersStep : LoaderStep
    {
        [DIInject] 
        private GameTreeController treeController;
        [DIInject] 
        private GameResourcesConfig resourcesConfig;
        [DIInject] 
        private GameplayConfig gameplayConfig;
        [DIInject] 
        private GameplayObjectsPool objectsPool;
        
        private IDIContainer diContainer;

        private bool isReady = false;
        
        public TransitionLoadGameControllersStep(IEventBus eventBus, IDIContainer diContainer) : base(eventBus)
        {
            this.diContainer = diContainer;
        }

        public override bool IsReady()
        {
            return isReady;
        }

        public override float GetProgress()
        {
            return isReady ? 1.0f : 0.0f;
        }
        
        public override string GetDescription()
        {
            return DefinedLocaleKeys.LoadingGameplay;
        }

        public override void PerformStep()
        {
            var diContext = diContainer.GetReference<GameplayContextsHolder>(null).GameContext;
            diContainer.Fetch(this, diContext);
            
            CreateGameTree(diContext);
            
            CreateSun(diContext);

            CreateBranchIndicator(diContext);
            
            isReady = true;
        }
        
        private void CreateGameTree(object diContext)
        {
            RemoveOldTree(diContext);
            
            var treePrefab = Resources.Load<GameTreeController>(resourcesConfig.TreePrefabPath);
            var treeInstance = GameObject.Instantiate<GameTreeController>(treePrefab, gameplayConfig.TreePosition, Quaternion.identity);
            diContainer.Register(treeInstance, diContext);

            GenerateElements(treeInstance, diContext);
        }
        
        private void RemoveOldTree(object diContext)
        {
            if (treeController != null)
            {
                GameObject.Destroy(treeController.gameObject);
                diContainer.Unregister(typeof(GameTreeController));
                diContainer.Unregister(typeof(GameTreeController), diContext);
            }
        }

        private void GenerateElements(GameTreeController treeInstance, object diContext)
        {
            var elementsCount = gameplayConfig.ElementsCount;
            GenerateBranches(treeInstance, elementsCount, diContext);
            GenerateRoots(treeInstance, elementsCount);
        }
        
        private void GenerateBranches(GameTreeController treeInstance, int count, object diContext)
        {
            var gameBranchesList = new GameBranchesList();
            diContainer.Register(gameBranchesList, diContext);
            
            var branchPrefab = Resources.Load<GameBranchController>(resourcesConfig.BranchPrefabPath);

            var branchSlots = new List<Transform>(treeInstance.GetBranchSlots());
            for (int i = 0; i < count; i++)
            {
                var branchSlotId = Random.Range(0, branchSlots.Count);
                var branchSlot = branchSlots[branchSlotId];
                branchSlots.RemoveAt(branchSlotId);

                var branchInstance = GameObject.Instantiate<GameBranchController>(branchPrefab, branchSlot);
                branchInstance.Initialize(eventBus, diContainer, diContext);
                ResetTransform(branchInstance.transform);
                branchSlots.AddRange(branchInstance.GetBranchSlots());
                gameBranchesList.Branches.Add(branchInstance);
            }
        }

        private void GenerateRoots(GameTreeController treeInstance, int count)
        {
            var rootPrefab = Resources.Load<GameRootController>(resourcesConfig.RootPrefabPath);

            var rootSlots = new List<Transform>(treeInstance.GetRootSlots());
            for (int i = 0; i < count; i++)
            {
                var rootSlotId = Random.Range(0, rootSlots.Count);
                var rootSlot = rootSlots[rootSlotId];
                rootSlots.RemoveAt(rootSlotId);

                var rootInstance = GameObject.Instantiate<GameRootController>(rootPrefab, rootSlot);
                ResetTransform(rootInstance.transform);
                rootSlots.AddRange(rootInstance.GetRootSlots());
            }
        }

        private void ResetTransform(Transform transform)
        {
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.identity;
            transform.localScale = Vector3.one;
        }
        
        private void CreateSun(object diContext)
        {
            var sunPrefab = Resources.Load<SunController>(resourcesConfig.SunPrefabPath);
            var sunController = GameObject.Instantiate(sunPrefab);
            diContainer.Register(sunController, diContext);
        }
        
        private void CreateBranchIndicator(object diContext)
        {
            var branchIndicator = Resources.Load<BranchIndicator>(resourcesConfig.BranchIndicatorPrefabPath);
            var branchIndicatorController = GameObject.Instantiate(branchIndicator);
            diContainer.Register(branchIndicatorController, diContext);
        }
    }
}