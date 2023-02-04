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
        [DIInject] 
        private GameplayBranchIndicatorData branchIndicatorData;

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
            branchInstance.Initialize(eventBus, diContainer, diContext, branchSlot, true);
            ResetTransform(branchInstance.transform);
            AddBranchToList(branchInstance);
        }

        private void GenerateRoot()
        {
            var rootPrefab = Resources.Load<GameRootController>(resourcesConfig.RootPrefabPath);
            
            var rootSlotId = Random.Range(0, gameRootsList.EmptySlots.Count);
            var rootSlot = gameRootsList.EmptySlots[rootSlotId];
            rootSlot.IsEmpty = false;
            gameRootsList.EmptySlots.RemoveAt(rootSlotId);

            var rootInstance = GameObject.Instantiate<GameRootController>(rootPrefab, rootSlot.Transform);
            rootInstance.StartGrowAnimation();
            ResetTransform(rootInstance.transform);
            AddRootToList(rootInstance);
        }
        
        private void ResetTransform(Transform transform)
        {
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.identity;
            transform.localScale = Vector3.one;
        }

        private void AddBranchToList(GameBranchController branchInstance)
        {
            bool added = false;
            for (int i = 0; !added && i < gameBranchesList.Branches.Count; i++)
            {
                if (gameBranchesList.Branches[i].GetStatsIconTransform().position.x > branchInstance.GetStatsIconTransform().position.x)
                {
                    if (i <= branchIndicatorData.CurrentBranchIndex)
                    {
                        branchIndicatorData.CurrentBranchIndex++;
                    }
                    gameBranchesList.Branches.Insert(i, branchInstance);
                    added = true;
                }
            }
            if (!added) gameBranchesList.Branches.Add(branchInstance);

             var emptySlots = branchInstance.GetBranchSlots();
            for (int i = 0; i < emptySlots.Count; i++)
            {
                if (emptySlots[i].Transform.position.y - gameplayConfig.ElementSize > gameplayConfig.GroundLevel)
                {
                    gameBranchesList.EmptySlots.Add(emptySlots[i]);
                }
            }
        }

        private void AddRootToList(GameRootController rootInstance)
        {
            bool added = false;
            for (int i = 0; !added && i < gameRootsList.Roots.Count; i++)
            {
                if (gameRootsList.Roots[i].transform.position.x > rootInstance.transform.position.x)
                {
                    gameRootsList.Roots.Insert(i, rootInstance);
                    added = true;
                }
            }
            if (!added) gameRootsList.Roots.Add(rootInstance);

            var emptySlots = rootInstance.GetRootSlots();
            for (int i = 0; i < emptySlots.Count; i++)
            {
                if (emptySlots[i].Transform.position.y + gameplayConfig.ElementSize < gameplayConfig.GroundLevel)
                {
                    gameRootsList.EmptySlots.Add(emptySlots[i]);
                }
            }
        }
    }
}