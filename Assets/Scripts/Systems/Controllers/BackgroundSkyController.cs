using System;
using BoxColliders.Project;
using UnityEngine;
using Utils;

namespace BoxColliders.Game
{
    public sealed class BackgroundSkyController : MonoBehaviour
    {
        [SerializeField] 
        private Animator animator;
        
        private void Awake()
        {
            ProjectDIContainer.Instance.Register(this, null);
        }

        public void SetAnimatorBool(bool animBool)
        {
            animator.SetBool(DefinedStrings.IsDay, animBool);
        }
    }
}