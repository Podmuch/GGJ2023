using System.Collections.Generic;
using UnityEngine;

namespace Controllers
{
    public class PlayerCharacterInputController : MonoBehaviour
    {
        [SerializeField] private CharacterMovementController playerCharacter;
        [SerializeField] private FixedJoystick leftJoystick;
        [SerializeField] public Transform cameraTransform;

        //Camera properties needed to transform the motion vector
        private Vector3 cameraForward;
        private Vector3 cameraRight;

        public FixedJoystick LeftJoystick
        {
            get => leftJoystick;
            set => leftJoystick = value;
        }

        public CharacterMovementController PlayerCharacter
        {
            get => playerCharacter;
            set => playerCharacter = value;
        }

        public Vector3 CameraForward
        {
            get => cameraForward;
            set => cameraForward = value;
        }

        public Vector3 CameraRight
        {
            get => cameraRight;
            set => cameraRight = value;
        }

        private void Awake()
        {
        }

        void Start()
        {
        }
        
        //Just for prototype
        private KeyCode returnButton(int i)
        {
            switch (i)
            {
                case 0:
                    return KeyCode.Q;
                case 1:
                    return KeyCode.W;
                case 2:
                    return KeyCode.E;
                case 3:
                    return KeyCode.R;
            }

            return KeyCode.Q;
        }

        // Update is called once per frame
        void Update()
        {
            var vInput = leftJoystick.Vertical;
            var hInput = leftJoystick.Horizontal;

            cameraForward = cameraTransform.forward;
            cameraRight = cameraTransform.right;
                
            Vector3 move = vInput * cameraForward + hInput * cameraRight;
            playerCharacter.Move(move);
        }
    }
}