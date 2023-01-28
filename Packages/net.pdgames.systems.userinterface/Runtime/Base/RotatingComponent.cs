using UnityEngine;

namespace PDGames.UserInterface
{
    public sealed class RotatingComponent : MonoBehaviour
    {
        public enum Axis { X, Y, Z }

        [SerializeField]
        private float speed = 90;
        [SerializeField]
        private Axis axis = Axis.Z;
        [SerializeField] 
        private bool isScaledTime = true;
        
        private Transform trans;

        private void Awake()
        {
            trans = transform;
        }

        private void Update()
        {
            float rotationSpeed = speed * (isScaledTime ? Time.deltaTime : Time.unscaledDeltaTime);
            Vector3 rotationVector = new Vector3(axis == Axis.X ? rotationSpeed : 0, 
                                                 axis == Axis.Y ? rotationSpeed : 0, 
                                                 axis == Axis.Z ? rotationSpeed : 0);
            trans.Rotate(rotationVector);
        }
    }
}