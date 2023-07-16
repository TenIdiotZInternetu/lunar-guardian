using System;
using Spawnables;
using UnityEngine;

namespace MovementPatterns
{
    [Serializable]
    public class LinearMovementPattern : MovementPattern
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
            OnValidate();
        }

        void OnValidate()
        {
            direction = Quaternion.Euler(0, 0, directionInDegrees) * Vector3.up;
            direction = direction.normalized;
        }

        public override Vector3 GetNextPosition(Entity entity)
        {
            Vector3 currentPosition = entity.transform.position;
            
            speed += acceleration * Time.deltaTime;
            return currentPosition + direction * (speed * Time.deltaTime);
        }
    
        void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.green;
            Vector3 position = transform.position;
            Gizmos.DrawLine(position, position + direction);
        }
    }
}
