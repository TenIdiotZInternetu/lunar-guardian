using System;
using MovementPatterns;
using UnityEngine;

namespace Spawnables
{
    public class Entity : MonoBehaviour
    {
        public float LifeTime => Time.time - _timeEnabled;
        public MovementPattern MovementPattern { get; set; }

        private float _timeEnabled;
        
        void OnEnable()
        {
            _timeEnabled = Time.time;
        }
        
        void Update()
        {
            transform.position = MovementPattern.GetNextPosition(this);
        }
    }
}