using System;
using BoxColliders.Configs;
using UnityEngine.SceneManagement;

namespace BoxColliders.Project
{
    [Serializable]
    public sealed class LoadedArenaDataHolder
    {
        public ArenaData Data;
        [NonSerialized] 
        public Scene LoadedScene = default;
    }
}