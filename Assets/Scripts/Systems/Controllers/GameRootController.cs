using System.Collections.Generic;
using UnityEngine;

namespace BoxColliders.Game
{
    public sealed class GameRootController : MonoBehaviour
    {
        [SerializeField]
        private List<SlotData> rootsSlots;

        public List<SlotData> GetRootSlots()
        {
            return rootsSlots;
        }
    }
}