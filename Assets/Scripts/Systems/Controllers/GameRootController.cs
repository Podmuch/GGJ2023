using System.Collections.Generic;
using UnityEngine;

namespace BoxColliders.Game
{
    public sealed class GameRootController : MonoBehaviour
    {
        [SerializeField]
        private List<Transform> rootsSlots;

        public List<Transform> GetRootSlots()
        {
            return rootsSlots;
        }
    }
}