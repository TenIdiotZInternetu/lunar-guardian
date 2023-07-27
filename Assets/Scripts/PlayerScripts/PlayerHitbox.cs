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
        [SerializeField] private GameObjectEvent onTakesHitEvent;
        
        public float invincibilityTime;
        
        private float _timeOfLastHit;


        private void Start()
        {
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            if (Time.time - _timeOfLastHit <= invincibilityTime) return;
            
            GameObject collidedObject = other.gameObject;
            bool isProjectile = collidedObject.CompareTag("EnemyProjectile");
            bool isEnemy = collidedObject.CompareTag("Enemy");
            
            if (!(isProjectile || isEnemy)) return; 
            
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