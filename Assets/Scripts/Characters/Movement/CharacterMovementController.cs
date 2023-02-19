using System;
using PDGames.DIContainer;
using UnityEngine;
using UnityEngine.Serialization;

namespace Controllers
{
    public class CharacterMovementController : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] 
        private Rigidbody _rigidbody;
        [SerializeField] 
        private Animator animator;
        [SerializeField] 
        private ParticleSystem shootParticles;
        
        //Movement parameters
        
        [Header("Movement Player properties")] [SerializeField]
        private float movingTurnSpeed = 360;
        [SerializeField] 
        private float stationaryTurnSpeed = 180;
        [SerializeField] 
        private float moveSpeedMultiplier = 1f;
        [SerializeField] 
        private float speedAnimationMultiplier = 1f;
        [SerializeField] 
        private float runAmountScaler = 3.78f;
        [SerializeField]
        private float walkAmount = 1.58f; 
        [SerializeField] 
        private float speedDampForward = 0.4f;
        [SerializeField]
        private float speedDampStopping = 0.4f;
        [SerializeField] 
        private float angularSpeedDamp = 0.2f;
        
        private float turnAmount;
        private float forwardAmount;
        private float previousForwardAmount;
        private int licznik;
        
        private IDIContainer diContainer;
        private object diContext;
        
        //Properties
        
        public Rigidbody Rigidbody
        {
            get => _rigidbody;
            set => _rigidbody = value;
        }
        
        public void Initialize(IDIContainer diContainer, object diContext)
        {
            this.diContainer = diContainer;
            this.diContext = diContext;
            diContainer.Fetch(this, diContext);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.K))
            {
                var Brian = GameObject.Find("Brian");
                Brian.GetComponent<Animator>().SetTrigger("Die");
            }
        }

        protected  void Start()
        {
            _rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY |
                                     RigidbodyConstraints.FreezeRotationZ;
           // gunFireController = GetComponentInChildren<GunfireController>();
        }

        //Move player method
        public void Move(Vector3 move)
        {
            if (move.magnitude > 1f) move.Normalize(); //set magnitude to 1
            move = transform.InverseTransformDirection(move);
            move = Vector3.ProjectOnPlane(move, Vector3.up);
            turnAmount = Mathf.Atan2(move.x, move.z);
            previousForwardAmount = forwardAmount;
            forwardAmount = move.z;
            
            ApplyRotatation();
            UpdateAnimator(move);
        }

        public void Hit()
        {
            
            var brian = GameObject.Find("Brian");

            if (licznik == 2)
            {
                licznik = 0;
            }

//            brian.GetComponent<Animator>().SetTrigger("Hit0" /*+ licznik.ToString()*/);

            licznik++;
        }
        
        
        
        public void PlayShoot()
        {
            return;
            if(Input.GetKey(KeyCode.Space));
            //gunFireController.FireWeapon();

            var brian = GameObject.Find("Brian");

            if (Vector3.Distance(transform.position, brian.transform.position) < 2f) ;
            brian.GetComponent<Animator>().SetTrigger("Die");
        }

        //Method which is helping to rotate player GO
        private void ApplyRotatation()
        {
            float turnSpeed = Mathf.Lerp(stationaryTurnSpeed, movingTurnSpeed, forwardAmount);
            transform.Rotate(0, turnAmount * turnSpeed * Time.deltaTime, 0);
        }

        void UpdateAnimator(Vector3 move)
        {
            //Here is scaling forward amount to be fitted with mecanim
            float forwardAmountScaled = forwardAmount * runAmountScaler;

            if (forwardAmountScaled != 0 && forwardAmountScaled < walkAmount)
            {
                forwardAmountScaled = walkAmount;
            }
            else if(forwardAmountScaled != 0)
            {
                forwardAmountScaled = runAmountScaler;
            }

            var speedDump = 0f;
            if (Mathf.Abs(forwardAmount) >= Mathf.Abs(previousForwardAmount))
            {
                speedDump = speedDampForward;
            }
            else
            {
                speedDump = speedDampStopping;
            }
            
            animator.SetFloat("Speed", forwardAmountScaled, speedDump, Time.deltaTime);
            animator.SetBool("Shoot", Input.GetKey(KeyCode.Space));

            if (Input.GetKey(KeyCode.Space))
            { 
                var brian = GameObject.Find("Brian"); 
                
             // transform.LookAt(brian.transform.position);  
            }
            
            if(Input.GetKeyDown(KeyCode.LeftShift)) animator.SetTrigger("Dash");
        }
    }
}
