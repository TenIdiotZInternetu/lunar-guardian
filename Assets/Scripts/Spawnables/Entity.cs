using System;
using MovementPatterns;
using UnityEngine;

namespace Spawnables
{
    public class Entity : MonoBehaviour
    {
        public MovementPattern MovementPattern { get; set; }

        void Update()
        {
            if (MovementPattern == null) return;
        
            transform.position = MovementPattern.GetNextPosition(transform.position);
        }
    }
}