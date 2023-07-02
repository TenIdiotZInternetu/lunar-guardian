using UnityEngine;

namespace MovementPatterns
{
    public class LinearMovementPattern : MonoBehaviour, IMovementPattern
    {
        [SerializeField]
        private float speed;
        [SerializeField]
        private float acceleration;
        [SerializeField]
        private float directionInDegrees;
        private Vector3 direction;

        void Start()
        {
            direction = Quaternion.Euler(0, 0, directionInDegrees) * Vector3.up;
        }
        
        public Vector3 GetNewPosition(Vector3 currentPosition)
        {
            return currentPosition + direction.normalized * (speed * Time.deltaTime);
        }
    }
}