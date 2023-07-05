using System;
using UnityEngine;

namespace MovementPatterns
{
    public class LinearMovementPattern : MonoBehaviour
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
        
        void Update()
        {
            speed += acceleration * Time.deltaTime;
            transform.position += direction * (speed * Time.deltaTime);
        }
        
        void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.green;
            Vector3 position = transform.position;
            Gizmos.DrawLine(position, position + direction);
        }
    }
}