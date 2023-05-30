using PDGames.DIContainer;
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

        private IDIContainer diContainer;
        private object diContext;
        
        public FixedJoystick LeftJoystick
        {
            get => leftJoystick;
            set => leftJoystick = value;
        }
        
        public Transform CameraTransform
        {
            get => cameraTransform;
            set => cameraTransform = value;
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

        public void Initialize(IDIContainer diContainer, object diContext)
        {
            this.diContainer = diContainer;
            this.diContext = diContext;
            diContainer.Fetch(this, diContext);
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
    }
}