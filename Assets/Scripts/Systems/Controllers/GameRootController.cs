using System.Collections.Generic;
using UnityEngine;

namespace BoxColliders.Game
{
    public sealed class GameRootController : MonoBehaviour
    {
        [SerializeField]
        private List<SlotData> rootsSlots;
        [SerializeField] 
        private Animator animator;

        public List<SlotData> GetRootSlots()
        {
            return rootsSlots;
        }
        
        public void ForceAnimationState(string animName)
        {
            if (animator == null) animator = GetComponent<Animator>();
            animator.Play(animName);
        }
    }
}