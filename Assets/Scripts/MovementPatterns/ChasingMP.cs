using System;
using Spawnables;
using UnityEngine;

namespace MovementPatterns
{
    public class ChasingMP : MovementPattern
    {
        [SerializeField] private GameObject target;
        [SerializeField] private float attractionRate;
        [SerializeField] private float initialDirection;

        [SerializeField] private float lockOnTime;
        [SerializeField] private float chaseTime;
        
        [SerializeField] private float speed;
        [SerializeField] private float acceleration;
        
        [SerializeField] private float simulationTimeStep = 0.1f;

        private Vector3 _initialMomentum;
        
        public override Vector3 GetNextPosition(Entity entity)
        {
            Vector3 currentPosition = entity.transform.position;
            Vector3 targetPosition = target.transform.position;
            float currentSpeed = speed + acceleration * entity.LifeTime;
            
            if (entity.LifeTime > chaseTime)
            {
                return currentPosition + entity.momentum;
            }

            Vector3 localMomentum = (entity.LifeTime < lockOnTime) ?
                _initialMomentum : 
                entity.momentum;
            
            return Interpolate(currentPosition, targetPosition, localMomentum);
        }

        private Vector3 Interpolate(Vector3 position, Vector3 target, Vector3 momentum)
        {
            Vector3 guideLine = target - position;
            Vector3 newExpectedPosition = position + guideLine.normalized * speed;
            Vector3 newLinearPosition = position + momentum;
            
            return Vector3.Lerp(newLinearPosition, newExpectedPosition, attractionRate / 100);
        }

        private void OnValidate()
        {
            Vector3 direction = Quaternion.Euler(0, 0, initialDirection) * Vector3.up;
            _initialMomentum = direction.normalized * speed;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            
            Vector3 newPosition = transform.position;
            Vector3 lastPosition = newPosition;
            Vector3 momentum = _initialMomentum;

            float timeFrame = 10;
            float timeStep = simulationTimeStep;
            
            for (float time = 0; time < timeFrame; time += timeStep)
            {
                newPosition = (time > chaseTime) ?
                    lastPosition + momentum :
                    Interpolate(newPosition, target.transform.position, momentum);
                
                Gizmos.DrawLine(lastPosition, newPosition);
                
                if (time > lockOnTime) momentum = newPosition - lastPosition;
                lastPosition = newPosition;
            }
        }
    }
}