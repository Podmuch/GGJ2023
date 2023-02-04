using UnityEngine;

namespace BoxColliders.Configs
{
    [CreateAssetMenu(fileName = "GameplaySmogCloudConfig", menuName = "Configs/GameplaySmogCloudConfig")]
    public class GameplaySmogCloudConfig : ScriptableObject
    {
        public const string ResourcePath = "Configs/GameplaySmogCloudConfig";

        public int MinBranchesForSmog;
        
        public float MinWaitingTime;
        public float MaxWaitingTime;

        public float MinMovingTime;
        public float MaxMovingTime;

        public Vector3 LeftPosition;
        public Vector3 RightPosition;
        public Vector3 WaitingPosition;
    }
}