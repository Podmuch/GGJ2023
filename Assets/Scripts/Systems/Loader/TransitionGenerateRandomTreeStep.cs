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

            var branchSlots = new List<SlotData>(treeInstance.GetBranchSlots());
            for (int i = 0; i < count; i++)
            {
                branchSlots.RemoveAll((br) => !br.IsEmpty);
                var branchSlotId = Random.Range(0, branchSlots.Count);
                var branchSlot = branchSlots[branchSlotId];
                branchSlot.IsEmpty = false;
                branchSlots.RemoveAt(branchSlotId);

                var branchInstance = GameObject.Instantiate<GameBranchController>(branchPrefab, branchSlot.Transform);
                branchInstance.DisableStateIcon();
                branchInstance.ForceAnimationState("Idle");
                ResetTransform(branchInstance.transform);
                AddEmptyBranchSlots(branchSlots, branchInstance);
            }
        }

        private void AddEmptyBranchSlots(List<SlotData> branchSlots, GameBranchController branchInstance)
        {
            var emptySlots = branchInstance.GetBranchSlots();
            for (int i = 0; i < emptySlots.Count; i++)
            {
                if (emptySlots[i].Transform.position.y - gameplayConfig.ElementSize > gameplayConfig.GroundLevel)
                {
                    branchSlots.Add(emptySlots[i]);
                }
            }
        }

        private void GenerateRoots(GameTreeController treeInstance, int count)
        {
            var rootPrefab = Resources.Load<GameRootController>(resourcesConfig.RootPrefabPath);

            var rootSlots = new List<SlotData>(treeInstance.GetRootSlots());
            for (int i = 0; i < count; i++)
            {
                rootSlots.RemoveAll((r) => !r.IsEmpty);
                var rootSlotId = Random.Range(0, rootSlots.Count);
                var rootSlot = rootSlots[rootSlotId];
                rootSlot.IsEmpty = false;
                rootSlots.RemoveAt(rootSlotId);

                var rootInstance = GameObject.Instantiate<GameRootController>(rootPrefab, rootSlot.Transform);
                rootInstance.ForceAnimationState("Root_Idle");
                ResetTransform(rootInstance.transform);
                AddEmptyRootSlots(rootSlots, rootInstance);
            }
        }

        private void AddEmptyRootSlots(List<SlotData> rootSlots, GameRootController rootInstance)
        {
            var emptySlots = rootInstance.GetRootSlots();
            for (int i = 0; i < emptySlots.Count; i++)
            {
                if (emptySlots[i].Transform.position.y + gameplayConfig.ElementSize < gameplayConfig.GroundLevel)
                {
                    rootSlots.Add(emptySlots[i]);
                }
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