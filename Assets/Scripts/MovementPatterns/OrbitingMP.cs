using System.Numerics;
using Spawnables;
using UnityEditor;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

namespace MovementPatterns
{
    public class OrbitingMP : MovementPattern
    {
        [SerializeField] private GameObject target;  
        [SerializeField] private float radius;
        
        [SerializeField] private float angularSpeed;
        [SerializeField] private float acceleration;

        public override Vector3 GetNextPosition(Entity entity)
        {
            float time = entity.LifeTime;
            Vector3 center = target.transform.position;
            
            float currentSpeed = angularSpeed + acceleration * time;
            float angle = currentSpeed * time;
            Vector3 slope = Quaternion.Euler(0, 0, angle) * Vector3.up * radius;

            return center + slope;
        }
        
        private void OnDrawGizmosSelected()
        {
            Handles.color = Color.green;
            Vector3 center = target.transform.position;
            Handles.DrawWireDisc(center, Vector3.forward, radius);
        }
    }
}