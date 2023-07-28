using System;
using System.Collections.Generic;
using PlayerScripts;
using UnityEngine;

namespace Spawnables.Collectibles
{
    public class Pickup : Entity
    {
        [Serializable]
        public class Reward
        {
            public PlayerStatus.ResourceType type;
            public int amount;
        }
        
        [SerializeField] public List<Reward> rewards;
        
        void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.gameObject.CompareTag("Player")) return;
            GiveRewards();
            ObjectPoolManager.Despawn(gameObject);
        }
        
        void GiveRewards()
        {
            foreach (Reward reward in rewards)
            {
                PlayerStatus.ChangeResource(reward.type, reward.amount);
            }
        }
    }
}