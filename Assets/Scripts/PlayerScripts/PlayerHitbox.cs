using System;
using Spawnables;
using UnityEngine;

namespace PlayerScripts
{
    public class PlayerHitbox : MonoBehaviour, IDamagable
    {
        public float invincibilityTime;
        
        public event EventHandler<GameObject> TakesHitEvent;
        
        private float _timeOfLastHit;
        
        void OnTriggerEnter2D(Collider2D other)
        {
            if (Time.time - _timeOfLastHit <= invincibilityTime) return;
            
            GameObject collidedObject = other.gameObject;
            bool isProjectile = collidedObject.CompareTag("EnemyProjectile");
            bool isEnemy = collidedObject.CompareTag("Enemy");
            
            if (!(isProjectile || isEnemy)) return; 
            
            TakesHitEvent?.Invoke(this, collidedObject);
            PlayerStatus.ChangeHealth(-1);
            _timeOfLastHit = Time.time;

            if (isProjectile)
            {
                ObjectPoolManager.Despawn(collidedObject);
            }
            
            if (isEnemy)
            {
                collidedObject.GetComponent<Enemy>().TakeDamage(50);
            }
        }
    }
}