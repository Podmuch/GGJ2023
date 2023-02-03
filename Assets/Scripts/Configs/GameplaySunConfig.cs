using System.Collections.Generic;
using BoxColliders.Game;
using UnityEngine;

namespace BoxColliders.Configs
{
    [CreateAssetMenu(fileName = "SunConfig", menuName = "Configs/SunConfig")]
    public sealed class GameplaySunConfig : ScriptableObject
    {
        public const string ResourcePath = "Configs/SunConfig";
        public List<SunPathData> sunPaths;
    }
}