using System;
using System.Collections.Generic;
using PlayerScripts;
using Projectiles;
using UnityEngine;

namespace Spawnables
{
    public class PlayerBulletSpawner : MonoBehaviour
    {
        public GameObject Projectile;
        public float Cooldown;

        private float _timeOfLastShot;
        
        public void Awake()
        {
            PlayerControls.PlayerShoots += OnPlayerShoots;
        }

        public void OnPlayerShoots(object sender, EventArgs e)
        {
            if (Time.time - _timeOfLastShot <= Cooldown) return;
            
            GameObject bullet = ObjectPoolManager.Spawn(Projectile, transform.position, Quaternion.identity);
            _timeOfLastShot = Time.time;
        }
    }
}