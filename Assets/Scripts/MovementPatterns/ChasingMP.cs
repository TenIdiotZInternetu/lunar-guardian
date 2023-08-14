using Spawnables;
using UnityEngine;

namespace MovementPatterns
{
    public class ChasingMP : MovementPattern
    {
        [SerializeField] private GameObject target;
        [SerializeField] private float lockOnTime;
        [SerializeField] private float chaseTime;
        
        [SerializeField] private float attractionRate;
        [SerializeField] private float speed;
        [SerializeField] private float acceleration;
        
        public override Vector3 GetNextPosition(Entity entity)
        {
            Transform entityTransform = entity.transform;
            
            Vector3 currentPosition = entityTransform.position;
            Quaternion currentRotation = entityTransform.rotation;
            float currentSpeed = speed + acceleration * entity.LifeTime;
            
            if (entity.LifeTime < lockOnTime || entity.LifeTime > lockOnTime + chaseTime)
            {
                return currentPosition + currentRotation * Vector3.up * (currentSpeed * Time.deltaTime);
            }

            Vector3 targetPosition = target.transform.position;
            Quaternion targetRotation = Quaternion.LookRotation(Vector3.forward, targetPosition - currentPosition);
            
            float rotationDifference = Quaternion.Angle(currentRotation, targetRotation);
            float distanceFromTarget = Mathf.Max(Vector3.Distance(currentPosition, targetPosition), 1.2f);

            float distanceFactor = 1 + 1 / Mathf.Log(distanceFromTarget, 5);
            float slerpRate = attractionRate * Time.deltaTime * rotationDifference * distanceFactor;
            
            Quaternion finalRotation = Quaternion.RotateTowards(currentRotation, targetRotation, slerpRate);
            Vector3 momentum = finalRotation * Vector3.up * (currentSpeed * Time.deltaTime);
            entityTransform.rotation = finalRotation;
            
            Vector3 finalPosition = currentPosition + momentum;
            return finalPosition;
        }
    }
}