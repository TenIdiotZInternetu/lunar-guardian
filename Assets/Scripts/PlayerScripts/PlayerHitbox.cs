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
        
        private bool _inBombState;
        private float _timeOfLastHit;

        public void AttemptHit(GameObject damageSource)
        {
            if (IsInvincible()) return;
            TakeHit(damageSource);
        }

        private bool IsInvincible()
        {
            return _inBombState || Time.time - _timeOfLastHit <= invincibilityTime;
        }
        
        void OnTriggerEnter2D(Collider2D other)
        {
            if (IsInvincible()) return;
            
            GameObject collidedObject = other.gameObject;
            bool isProjectile = collidedObject.CompareTag("EnemyProjectile");
            bool isEnemy = collidedObject.CompareTag("Enemy");
            
            if (!(isProjectile || isEnemy)) return; 
            
            TakeHit(collidedObject);

            if (isProjectile)
            {
                ObjectPoolManager.Despawn(collidedObject);
            }
            
            if (isEnemy)
            {
                collidedObject.GetComponent<Enemy>().TakeDamage(50, this.gameObject);
            }
        }

        private void TakeHit(GameObject damageSource)
        {
            PlayerStatus.ResetScoreMultiplier();
            PlayerStatus.ChangeHealth(-1);
            
            onTakesHitEvent?.Invoke(damageSource);
            _timeOfLastHit = Time.time;
        }
        
        public void ChangeBombState(bool state)
        {
            _inBombState = state;
        }
    }
}