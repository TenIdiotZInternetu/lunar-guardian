using UnityEngine;

namespace MovementPatterns
{
    public class LinearMovementPattern : IMovementPattern
    {
        private float _speed;
        private Vector3 _direction;

        public LinearMovementPattern(float speed, Vector3 direction)
        {
            _speed = speed;
            _direction = direction;
        }

        public Vector3 GetNewPosition(Vector3 currentPosition)
        {
            return currentPosition + _direction.normalized * (_speed * Time.deltaTime);
        }
    }
}