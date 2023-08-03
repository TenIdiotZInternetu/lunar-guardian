using Spawnables;
using UnityEngine;

namespace MovementPatterns
{
    public class ChasingMP : MovementPattern
    {
        [SerializeField] private GameObject target;
        [SerializeField] private float attractionRate;
        [SerializeField] private float chaseTime;
        [SerializeField] private float initialDirection;
        
        [SerializeField] private float speed;
        [SerializeField] private float acceleration;
        
        public override Vector3 GetNextPosition(Entity entity)
        {
            Vector3 currentPosition = entity.transform.position;
            Vector3 targetPosition = target.transform.position;
            float currentSpeed = speed + acceleration * entity.LifeTime;
            
            if (entity.LifeTime > chaseTime)
            {
                return currentPosition + entity.momentum * currentSpeed;
            }
            
            Vector3 localMomentum = entity.momentum;
            
            if (entity.LifeTime < 0.5f)
            {
                Vector3 direction = Quaternion.Euler(0, 0, initialDirection) * Vector3.up;
                localMomentum = direction.normalized * currentSpeed;
            }

            Vector3 guideLine = targetPosition - currentPosition;
            Vector3 expectedPosition = currentPosition + guideLine.normalized * currentSpeed;
            Vector3 momentumContinuation = currentPosition + localMomentum * currentSpeed;
            
            Vector3 finalPosition = Vector3.Lerp(momentumContinuation, expectedPosition, attractionRate);
            return finalPosition;
        }
        
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.green;
            
            Vector3 newPosition = transform.position;
            Vector3 lastPosition = newPosition;
            
            
        }
    }
}