using System;
using System.Collections.Generic;
using MovementPatterns;
using UnityEngine;

namespace Projectiles
{
    public class BasicPellet : MonoBehaviour
    {
        public float movementSpeed;
        public Vector3 direction;
        public float cooldown;
        
        private IMovementPattern _movementPattern;
        

        // Start is called before the first frame update
        void Start()
        {
            _movementPattern = GetComponent<IMovementPattern>();
        }

        // Update is called once per frame
        void Update()
        {
            transform.position = _movementPattern.GetNewPosition(transform.position);
        }
    }
}