using System.Collections.Generic;
using BoxColliders.Game;
using UnityEngine;

namespace BoxColliders.Configs
{
    [CreateAssetMenu(fileName = "DayNightCycleConfig", menuName = "Configs/DayNightCycleConfig")]
    public sealed class GameplayDayNightCycleConfig : ScriptableObject
    {
        public const string ResourcePath = "Configs/DayNightCycleConfig";
        public List<SunPathData> sunPaths;

        public Vector2 minMaxDayTime;
        public Vector2 minMaxNightTime;
    }
}