using UnityEngine;

namespace BoxColliders.Configs
{
    [CreateAssetMenu(fileName = "GameResourcesConfig", menuName = "Configs/GameResourcesConfig")]
    public sealed class GameResourcesConfig : ScriptableObject
    {
        public const string ResourcePath = "Configs/GameResourcesConfig";

        public string TreePrefabPath;
        public string BranchPrefabPath;
        public string RootPrefabPath;
    }
}