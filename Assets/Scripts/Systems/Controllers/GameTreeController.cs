using System.Collections.Generic;
using UnityEngine;

namespace BoxColliders.Game
{
    public sealed class GameTreeController : MonoBehaviour
    {
        [SerializeField] 
        private List<SlotData> branchSlots;
        
        [SerializeField] 
        private List<SlotData> rootSlots;

        public List<SlotData> GetBranchSlots()
        {
            return branchSlots;
        }

        public List<SlotData> GetRootSlots()
        {
            return rootSlots;
        }
    }
}