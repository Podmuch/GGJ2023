using System;
using UnityEngine;

namespace BoxColliders.Game
{
    [Serializable]
    public class SlotData
    {
        public Transform Transform;
        [NonSerialized]
        public bool IsEmpty = true;
        [NonSerialized] 
        public GameBranchController BranchController;
    }
}