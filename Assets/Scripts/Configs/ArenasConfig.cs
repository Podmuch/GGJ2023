using System.Collections.Generic;
using UnityEngine;

namespace BoxColliders.Configs
{
    [CreateAssetMenu(fileName = "ArenasConfig", menuName = "Configs/ArenasConfig")]
    public sealed class ArenasConfig : ScriptableObject
    {
        public const string ResourcePath = "Configs/ArenasConfig";
        
        public List<ArenaData> Arenas;
    }
}