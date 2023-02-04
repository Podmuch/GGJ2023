using System.Collections.Generic;
using BoxColliders.Configs;
using BoxColliders.Game;
using PDGames.DIContainer;
using PDGames.EventBus;
using PDGames.Systems.Loader;
using UnityEngine;
using Utils;

namespace BoxColliders.Project
{
    public sealed class TransitionGenerateRandomTreeStep : LoaderStep
    {
        [DIInject] 
        private GameTreeController treeController;
        [DIInject] 
        private GameResourcesConfig resourcesConfig;
        [DIInject] 
        private GameplayConfig gameplayConfig;
        
        private IDIContainer diContainer;

        public TransitionGenerateRandomTreeStep(IEventBus eventBus, IDIContainer diContainer) : base(eventBus)
        {
            this.diContainer = diContainer;
        }

        public override bool IsReady()
        {
            return true;
        }

        public override float GetProgress()
        {
            return 1.0f;
        }

        public override string GetDescription()
        {
            return DefinedLocaleKeys.InterfaceInitialization;
        }

        public override void PerformStep()
        {
            diContainer.Fetch(this);

            RemoveOldTree();
            
            var treePrefab = Resources.Load<GameTreeController>(resourcesConfig.TreePrefabPath);
            var treeInstance = GameObject.Instantiate<GameTreeController>(treePrefab, gameplayConfig.TreePosition, Quaternion.identity);
            diContainer.Register(treeInstance);

            var randomElementsCount = Random.Range(10, 20);
            GenerateBranches(treeInstance, randomElementsCount);
            GenerateRoots(treeInstance, randomElementsCount);
        }

        private void RemoveOldTree()
        {
            if (treeController != null)
            {
                GameObject.Destroy(treeController.gameObject);
                diContainer.Unregister(typeof(GameTreeController));
            }
        }
        
        private void GenerateBranches(GameTreeController treeInstance, int count)
        {
            var branchPrefab = Resources.Load<GameBranchController>(resourcesConfig.BranchPrefabPath);

            var branchSlots = new List<Transform>(treeInstance.GetBranchSlots());
            for (int i = 0; i < count; i++)
            {
                var branchSlotId = Random.Range(0, branchSlots.Count);
                var branchSlot = branchSlots[branchSlotId];
                branchSlots.RemoveAt(branchSlotId);

                var branchInstance = GameObject.Instantiate<GameBranchController>(branchPrefab, branchSlot);
                branchInstance.DisableStateIcon();
                ResetTransform(branchInstance.transform);
                branchSlots.AddRange(branchInstance.GetBranchSlots());
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
    }
}