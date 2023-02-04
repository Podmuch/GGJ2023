using System;

namespace BoxColliders.Game
{
    [Serializable]
    public class GameBranchStateData
    {
        public BranchState State;

        public bool IsClosedToWeather;
        
        public bool IsTakingWater;
        public bool IsTakingSun;
        public bool isTakingAir;
    }
}