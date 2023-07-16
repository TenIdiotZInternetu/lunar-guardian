using System;
using PlayerScripts;
using UnityEngine;

namespace Spawnables.Projectiles
{
    public class BulletSpawner : MonoBehaviour
    {
        public GameObject projectile;
        public float cooldown;
        public bool isPlayers;
        
        public float chargeTime;
        public int bulletsInCharge;

        private float _timeOfLastShot;
        private int _bulletsShot = 0;
    
        public void Awake()
        {
            if (isPlayers) Controls.Action1 += Shoot;
        }

        public void Shoot(object sender, EventArgs e)
        {
            if (_bulletsShot >= bulletsInCharge) Recharge();
            if (Time.time - _timeOfLastShot <= cooldown) return;
        
            ObjectPoolManager.Spawn(projectile, transform.position, Quaternion.identity);
            _timeOfLastShot = Time.time;
            _bulletsShot++;
        }

        private void Recharge()
        {
            if (Time.time - _timeOfLastShot <= chargeTime) return;
            _bulletsShot = 0;
        }

    }
}
