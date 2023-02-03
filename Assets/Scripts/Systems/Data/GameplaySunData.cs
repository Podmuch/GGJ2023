using System;
using UnityEngine;

namespace BoxColliders.Game
{
    public sealed class GameplaySunData
    {
        public bool isMoving;
        public SunPathData currentSunPath;
        public float sunFactor;
    }

    [Serializable]
    public class SunPathData
    {
        public AnimationCurve xCurve;
        public AnimationCurve yCurve;
        public float speed;

        public Vector2 Evaluate(float factor)
        {
            var x = xCurve.Evaluate(factor);
            var y = yCurve.Evaluate(factor);
            return new Vector2(x, y);
        }
    }
}