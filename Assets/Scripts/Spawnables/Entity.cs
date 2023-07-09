using System;
using MovementPatterns;
using UnityEngine;

namespace Spawnables
{
    public class Entity : MonoBehaviour
    {
        public float LifeTime => Time.time - _timeEnabled;
        public float Health;
        public MovementPattern MovementPattern { get; set; }

        private float _timeEnabled;
        
        void OnEnable()
        {
            _timeEnabled = Time.time;
        }
        
        void Update()
        {
            if (MovementPattern == null) return;
        
            transform.position = MovementPattern.GetNextPosition(this);
        }
    }
}