using System;

namespace BoxColliders.Game
{
    public class GameBranchStateData
    {
        public BranchState State;

        public bool IsClosedToWeather;
        
        public bool IsTakingWater;
        public bool IsTakingSun;
        public bool IsTakingAir;
        public bool IsInSmog;

        public float Health;
    }
}