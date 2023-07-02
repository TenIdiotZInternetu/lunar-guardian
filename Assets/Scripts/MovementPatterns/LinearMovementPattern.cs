using UnityEngine;

namespace MovementPatterns
{
    public class LinearMovementPattern : MonoBehaviour, IMovementPattern
    {
        [SerializeField]
        private float speed;
        [SerializeField]
        private Vector3 direction;

        public Vector3 GetNewPosition(Vector3 currentPosition)
        {
            return currentPosition + direction.normalized * (speed * Time.deltaTime);
        }
    }
}