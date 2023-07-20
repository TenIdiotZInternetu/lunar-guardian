using PlayerScripts;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Spawnables.Collectibles
{
    public class HealthPickup : Collectible
    {
        public int healthBonus = 1;
        public int scoreBonus = 500;
        
        void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.gameObject.CompareTag("Player")) return;
            
            PlayerStatus.ChangeHealth(healthBonus);
            PlayerStatus.IncreaseScore(scoreBonus);
            ObjectPoolManager.Despawn(gameObject);
        }
    }
}