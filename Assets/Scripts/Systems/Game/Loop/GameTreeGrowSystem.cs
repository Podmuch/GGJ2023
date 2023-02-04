using System.Collections.Generic;
using BoxColliders.Configs;
using PDGames.DIContainer;
using PDGames.EventBus;
using PDGames.Systems;
using UnityEngine;

namespace BoxColliders.Game
{
    public sealed class GameTreeGrowSystem : ReactiveSystem<GameTreeGrowEvent>, IInitializeSystem
    {
        [DIInject] 
        private GameTreeController treeController;
        [DIInject] 
        private GameResourcesConfig resourcesConfig;
        [DIInject] 
        private GameplayConfig gameplayConfig;
        [DIInject] 
        private GameBranchesList gameBranchesList;
        [DIInject]
        private GameRootsList gameRootsList;

        private IDIContainer diContainer;
        private object diContext;
        
        public GameTreeGrowSystem(IEventBus eventBus, IDIContainer diContainer, object diContext) : base(eventBus)
        {
            this.diContainer = diContainer;
            this.diContext = diContext;
        }

        public void Initialize()
        {
            diContainer.Fetch(this, diContext);
        }

        protected override void Execute(List<GameTreeGrowEvent> events)
        {
            for (int i = 0; i < events.Count; i++)
            {
                events[i].IsDestroyed = true;
                AddNextTreeElement();
            }
        }

        private void AddNextTreeElement()
        {
            GenerateBranch();
            GenerateRoot();
        }

        private void GenerateBranch()
        {
            var branchPrefab = Resources.Load<GameBranchController>(resourcesConfig.BranchPrefabPath);
            
            var branchSlotId = Random.Range(0, gameBranchesList.EmptySlots.Count);
            var branchSlot = gameBranchesList.EmptySlots[branchSlotId];
            branchSlot.IsEmpty = false;
            gameBranchesList.EmptySlots.RemoveAt(branchSlotId);
            
            var branchInstance = GameObject.Instantiate<GameBranchController>(branchPrefab, branchSlot.Transform);
            branchInstance.Initialize(eventBus, diContainer, diContext);
            ResetTransform(branchInstance.transform);
            gameBranchesList.EmptySlots.AddRange(branchInstance.GetBranchSlots());
            gameBranchesList.Branches.Add(branchInstance);
        }

        private void GenerateRoot()
        {
            var rootPrefab = Resources.Load<GameRootController>(resourcesConfig.RootPrefabPath);
            
            var rootSlotId = Random.Range(0, gameRootsList.EmptySlots.Count);
            var rootSlot = gameRootsList.EmptySlots[rootSlotId];
            rootSlot.IsEmpty = false;
            gameRootsList.EmptySlots.RemoveAt(rootSlotId);

            var rootInstance = GameObject.Instantiate<GameRootController>(rootPrefab, rootSlot.Transform);
            ResetTransform(rootInstance.transform);
            gameRootsList.EmptySlots.AddRange(rootInstance.GetRootSlots());
            gameRootsList.Roots.Add(rootInstance);
        }
        
        private void ResetTransform(Transform transform)
        {
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.identity;
            transform.localScale = Vector3.one;
        }
    }
}