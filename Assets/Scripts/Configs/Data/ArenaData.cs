using System;
using UnityEngine;

namespace BoxColliders.Configs
{
    [Serializable]
    public sealed class ArenaData
    {
        public string ArenaName;
        
        public Vector3 CameraPosition;
        public Vector3 CameraRotation;

        public float CameraFarClipPlane;
    }
}