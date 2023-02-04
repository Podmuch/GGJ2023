using UnityEngine;

namespace Systems.Data
{
    public class GameplaySmogCloudData
    {
        public bool IsWaiting;
        public float WaitingTimer;
        public float WaitingDuration;

        public bool IsMoving;
        public float MovingTimer;
        public float MovementDuration;
        
        public Vector3 StartPosition;
        public Vector3 EndPosition;
    }
}