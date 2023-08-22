using System;
using MovementPatterns;
using UnityEditor;
using UnityEngine;

namespace Spawnables
{
    public class Entity : MonoBehaviour
    {
        public float LifeTime => Time.time - _timeEnabled;
        
        public MovementPattern movementPattern;
        public string SpawnKey;
        
        
        private float _timeEnabled;
        
        void OnEnable()
        {
            _timeEnabled = Time.time;
        }
        
        public void Update()
        {
            if (movementPattern != null)
            {
                transform.position = movementPattern.GetNextPosition(this);
            }
        }
        
        public void ChangeMovementPattern(MovementPattern newMovementPattern)
        {
            movementPattern = newMovementPattern;
        }
    }
}