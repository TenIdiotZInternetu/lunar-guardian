using System;
using MovementPatterns;
using UnityEditor;
using UnityEngine;

namespace Spawnables
{
    public class Entity : MonoBehaviour
    {
        public float LifeTime => Time.time - _timeEnabled;
        public Vector3 momentum { get; private set; }
        
        public MovementPattern movementPattern;
        public string SpawnKey;
        
        
        private float _timeEnabled;
        
        void OnEnable()
        {
            _timeEnabled = Time.time;
        }
        
        public void Update()
        {
            Vector3 previousPosition = transform.position;
            transform.position = movementPattern.GetNextPosition(this);
            momentum = transform.position - previousPosition;
        }
        
        public void ChangeMovementPattern(MovementPattern newMovementPattern)
        {
            movementPattern = newMovementPattern;
        }
    }
}