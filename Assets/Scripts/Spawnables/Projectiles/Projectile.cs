using System;
using MovementPatterns;
using PlayerScripts;
using Unity.VisualScripting;
using UnityEngine;

namespace Spawnables.Projectiles
{
    public class Projectile : Entity
    {
        public int Damage;

        void Start()
        {
            BombController.OnBombDamageTick += (_) => Disperse();
        }

        private void Disperse()
        {
            ObjectPoolManager.Despawn(gameObject);
        }
    }
}