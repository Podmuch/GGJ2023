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
            CreateCloudController(diContext);
            CreateSmogController(diContext);

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
            GenerateRoots(treeInstance, elementsCount, diContext);
        }
        
        private void GenerateBranches(GameTreeController treeInstance, int count, object diContext)
        {
            var gameBranchesList = new GameBranchesList();
            diContainer.Register(gameBranchesList, diContext);
            
            var branchPrefab = Resources.Load<GameBranchController>(resourcesConfig.BranchPrefabPath);

            gameBranchesList.EmptySlots.AddRange(treeInstance.GetBranchSlots());
            for (int i = 0; i < count; i++)
            {
                var branchSlotId = Random.Range(0, gameBranchesList.EmptySlots.Count);
                var branchSlot = gameBranchesList.EmptySlots[branchSlotId];
                branchSlot.IsEmpty = false;
                gameBranchesList.EmptySlots.RemoveAt(branchSlotId);

                var branchInstance = GameObject.Instantiate<GameBranchController>(branchPrefab, branchSlot.Transform);
                branchInstance.Initialize(eventBus, diContainer, diContext, branchSlot);
                ResetTransform(branchInstance.transform);
                AddBranchToList(gameBranchesList, branchInstance);
            }
        }

        private void GenerateRoots(GameTreeController treeInstance, int count, object diContext)
        {
            var gameRootsList = new GameRootsList();
            diContainer.Register(gameRootsList, diContext);
            
            var rootPrefab = Resources.Load<GameRootController>(resourcesConfig.RootPrefabPath);

            gameRootsList.EmptySlots.AddRange(treeInstance.GetRootSlots());
            for (int i = 0; i < count; i++)
            {
                var rootSlotId = Random.Range(0, gameRootsList.EmptySlots.Count);
                var rootSlot = gameRootsList.EmptySlots[rootSlotId];
                rootSlot.IsEmpty = false;
                gameRootsList.EmptySlots.RemoveAt(rootSlotId);

                var rootInstance = GameObject.Instantiate<GameRootController>(rootPrefab, rootSlot.Transform);
                rootInstance.ForceAnimationState("Root_Idle");
                ResetTransform(rootInstance.transform);
                AddRootToList(gameRootsList, rootInstance);
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

        private void CreateCloudController(object diContext)
        {
            var cloudPrefab = Resources.Load<RainCloudController>(resourcesConfig.RainCloudPath);

            var cloudInstance = GameObject.Instantiate<RainCloudController>(cloudPrefab);
            diContainer.Register(cloudInstance, diContext);
        }

        private void CreateSmogController(object diContext)
        {
            var smogPrefab = Resources.Load<SmogCloudController>(resourcesConfig.SmogCloudPath);

            var smogInstance = GameObject.Instantiate<SmogCloudController>(smogPrefab);
            diContainer.Register(smogInstance, diContext);
        }
        
        private void CreateBranchIndicator(object diContext)
        {
            var branchIndicator = Resources.Load<BranchIndicator>(resourcesConfig.BranchIndicatorPrefabPath);
            var branchIndicatorController = GameObject.Instantiate(branchIndicator);
            diContainer.Register(branchIndicatorController, diContext);
        }

        private void AddBranchToList(GameBranchesList gameBranchesList, GameBranchController branchInstance)
        {
            bool added = false;
            for (int i = 0; !added && i < gameBranchesList.Branches.Count; i++)
            {
                if (gameBranchesList.Branches[i].GetStatsIconTransform().position.x > branchInstance.GetStatsIconTransform().position.x)
                {
                    gameBranchesList.Branches.Insert(i, branchInstance);
                    added = true;
                }
            }
            if (!added) gameBranchesList.Branches.Add(branchInstance);
            
            if (gameBranchesList.BestBranchesCount < gameBranchesList.Branches.Count)
            {
                gameBranchesList.BestBranchesCount = gameBranchesList.Branches.Count;
            }

            var emptySlots = branchInstance.GetBranchSlots();
            for (int i = 0; i < emptySlots.Count; i++)
            {
                if (emptySlots[i].Transform.position.y - gameplayConfig.ElementSize > gameplayConfig.GroundLevel)
                {
                    gameBranchesList.EmptySlots.Add(emptySlots[i]);
                }
            }
        }

        private void AddRootToList(GameRootsList gameRootsList, GameRootController rootInstance)
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