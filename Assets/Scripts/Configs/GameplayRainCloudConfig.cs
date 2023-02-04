using UnityEngine;

namespace BoxColliders.Configs
{
    [CreateAssetMenu(fileName = "GameplayRainCloudConfig", menuName = "Configs/GameplayRainCloudConfig")]
    public class GameplayRainCloudConfig  : ScriptableObject
    {
        public const string ResourcePath = "Configs/GameplayRainCloudConfig";

        public float MinWaitingTime;
        public float MaxWaitingTime;

        public float MinMovingTime;
        public float MaxMovingTime;

        public Vector3 LeftPosition;
        public Vector3 RightPosition;
        public Vector3 WaitingPosition;
    }
}