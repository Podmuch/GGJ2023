using System.Collections.Generic;

namespace BoxColliders.Game
{
    public class GameBranchesList
    {
        public int BestBranchesCount;
        
        public List<GameBranchController> Branches = new List<GameBranchController>();

        public List<SlotData> EmptySlots = new List<SlotData>();
    }
}