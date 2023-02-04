using System.Collections.Generic;
using UnityEngine;

namespace BoxColliders.Configs
{
    [CreateAssetMenu(fileName = "GameplayConfig", menuName = "Configs/GameplayConfig")]
    public sealed class GameplayConfig : ScriptableObject
    {
        public const string ResourcePath = "Configs/GameplayConfig";

        [Header("Water")]
        public float MaxWaterCapacity;
        public float BranchWaterProduction;
        public float WaterToEnergyConversion;

        [Header("Sun")] 
        public float MaxSunCapacity;
        public float BranchSunProduction;
        public float SunToEnergyConversion;

        [Header("Air")] 
        public float MaxAirCapacity;
        public float BranchAirProduction;
        public float AirToEnergyConversion;

        [Header("Growing")] 
        public float EnergyProductionInterval;
        public int EnergyForGrow;

        [Header("Smog")] 
        public float MaxBranchHealth;
        public float SmogHealthConsumption;
        
        [Header("Starting Params")] 
        public int ElementsCount;
        
        [Header("Map")] 
        public Vector3 TreePosition;
        public float GroundLevel;
        public float ElementSize;
    }
}