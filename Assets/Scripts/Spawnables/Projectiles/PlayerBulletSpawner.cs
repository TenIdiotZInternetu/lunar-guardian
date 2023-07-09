using System;
using PlayerScripts;
using UnityEngine;

namespace Spawnables.Projectiles
{
    public class PlayerBulletSpawner : MonoBehaviour
    {
        public GameObject Projectile;
        public float Cooldown;

        private float _timeOfLastShot;
    
        public void Awake()
        {
            Controls.Action1 += OnPlayerShoots;
        }

        public void OnPlayerShoots(object sender, EventArgs e)
        {
            if (Time.time - _timeOfLastShot <= Cooldown) return;
        
            ObjectPoolManager.Spawn(Projectile, transform.position, Quaternion.identity);
            _timeOfLastShot = Time.time;
        }
    }
}
