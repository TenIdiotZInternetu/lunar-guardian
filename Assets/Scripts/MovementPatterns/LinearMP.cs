using System;
using MyBox;
using Spawnables;
using UnityEngine;

namespace MovementPatterns
{
    [Serializable]
    public class LinearMP : MovementPattern
    {
        [SerializeField] 
        private float speed;
        [SerializeField]
        private float acceleration;
        [SerializeField] 
        private bool followsRotation;
        [SerializeField, ConditionalField(nameof(followsRotation), inverse: true)]
        private float directionInDegrees;
    
        private Vector3 direction;
        
        void OnEnable()
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
            Vector3 localDirection = direction;
            
            if (followsRotation)
            {
                float rotation = entity.transform.rotation.eulerAngles.z;
                localDirection = Quaternion.Euler(0, 0, rotation) * Vector3.up;
                localDirection = localDirection.normalized;
            }
            
            Vector3 currentPosition = entity.transform.position;
            
            float currentSpeed = speed + acceleration * entity.LifeTime;
            return currentPosition + localDirection * (currentSpeed * Time.deltaTime);
        }
    
        void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.green;
            Vector3 position = transform.position;
            Gizmos.DrawLine(position, position + direction);
        }
    }
}
