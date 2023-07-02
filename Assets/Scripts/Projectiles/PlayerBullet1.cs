using System;
using System.Collections.Generic;
using MovementPatterns;
using UnityEngine;

namespace Projectiles
{
    public class PlayerBullet1 : MonoBehaviour, IPlayerProjectile
    {
        public float movementSpeed;
        public Vector3 direction;
        public float cooldown;
        public int damage;
        public List<GameObject> spawners;

        private float _cooldownTimer = 0;
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
            _cooldownTimer -= Time.deltaTime;
        }
        
        public void OnPlayerShoots(object sender, EventArgs e)
        {
            if (_cooldownTimer >= 0) return;

            foreach (var spawner in spawners)
            {
                Instantiate(this, spawner.transform.position, Quaternion.identity);
            }
            
            _cooldownTimer = cooldown;
        }
    }
}