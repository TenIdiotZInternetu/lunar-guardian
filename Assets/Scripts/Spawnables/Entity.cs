using System;
using MovementPatterns;
using UnityEngine;

namespace Spawnables
{
    public class Entity : MonoBehaviour
    {
        public float LifeTime => Time.time - _timeEnabled;
        public MovementPattern MovementPattern;

        private float _timeEnabled;
        
        void OnEnable()
        {
            _timeEnabled = Time.time;
        }
        
        public void Update()
        {
            transform.position = MovementPattern.GetNextPosition(this);
        }
    }
}