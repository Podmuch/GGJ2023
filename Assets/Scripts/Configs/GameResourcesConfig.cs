using System;
using System.Collections.Generic;
using BoxColliders.Game;
using UnityEngine;

namespace BoxColliders.Configs
{
    [CreateAssetMenu(fileName = "GameResourcesConfig", menuName = "Configs/GameResourcesConfig")]
    public sealed class GameResourcesConfig : ScriptableObject
    {
        [Serializable]
        private class BranchStateIcon
        {
            public BranchState State;
            public Sprite Icon;
        }
        
        public const string ResourcePath = "Configs/GameResourcesConfig";

        public string TreePrefabPath;
        public string BranchPrefabPath;
        public string BranchIndicatorPrefabPath;
        public string SunPrefabPath;
        public string RainCloudPath;
        public string RootPrefabPath;

        [SerializeField]
        private List<BranchStateIcon> stateIcons;

        public Sprite GetStateIcon(BranchState state)
        {
            return stateIcons.Find((si) => si.State == state).Icon;
        }
    }
}