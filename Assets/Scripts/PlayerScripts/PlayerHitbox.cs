using System;
using Spawnables;
using Spawnables.VFX;
using Tools;
using UnityEngine;
using UnityEngine.Events;

namespace PlayerScripts
{
    public class PlayerHitbox : MonoBehaviour
    {
        public float invincibilityTime;
        [SerializeField] private GameObjectEvent onTakesHitEvent;
        
        private float _timeOfLastHit;


        void OnTriggerEnter2D(Collider2D other)
        {
            if (Time.time - _timeOfLastHit <= invincibilityTime) return;
            
            GameObject collidedObject = other.gameObject;
            bool isProjectile = collidedObject.CompareTag("EnemyProjectile");
            bool isEnemy = collidedObject.CompareTag("Enemy");
            
            if (!(isProjectile || isEnemy)) return; 
            
            PlayerStatus.ChangeHealth(-1);
            onTakesHitEvent?.Invoke(collidedObject);
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