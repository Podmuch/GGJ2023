using System.Collections.Generic;
using UnityEngine;

namespace BoxColliders.Game
{
    public sealed class GameTreeController : MonoBehaviour
    {
        [SerializeField] 
        private List<Transform> branchSlots;
        
        [SerializeField] 
        private List<Transform> rootSlots;

        public List<Transform> GetBranchSlots()
        {
            return branchSlots;
        }

        public List<Transform> GetRootSlots()
        {
            return rootSlots;
        }
    }
}