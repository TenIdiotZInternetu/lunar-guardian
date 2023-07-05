using System;
using System.Collections.Generic;
using MovementPatterns;
using UnityEngine;

namespace Projectiles
{
    public class BasicPellet : MonoBehaviour
    {
        public float Damage;
        private IMovementPattern _movementPattern;

        // Start is called before the first frame update
        void OnEnable()
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