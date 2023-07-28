using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using MovementPatterns;
using PlayerScripts;
using Spawnables;
using Spawnables.Projectiles;
using Tools;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : Entity
{
    [Serializable]
    public class LootDrop
    {
        public GameObject item;
        public float dropChance;
    }
    
    public List<BulletSpawner> bulletSpawners;
    public int maxHealth;

    public int scoreReward;
    public List<LootDrop> drops;

    public event Action ShootsEvent;
    
    [SerializeField] private GameObjectEvent onTakesHitEvent;

    private int _currentHealth;

    void Start()
    {
        _currentHealth = maxHealth;
        
        foreach (var spawner in bulletSpawners)
        {
            ShootsEvent += spawner.Shoot;
        }
        
        BombController.OnBombDamageTick += (damage) => TakeDamage(damage, Player.Instance.gameObject);
    }
    
    void Update()
    {
        base.Update();
        ShootsEvent?.Invoke();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        GameObject collidedObject = other.gameObject;
        if (!collidedObject.CompareTag("PlayerProjectile")) return;
        
        var projectile = collidedObject.GetComponent<Projectile>();
        TakeDamage(projectile.Damage, collidedObject);
        ObjectPoolManager.Despawn(collidedObject);
    }

    public void TakeDamage(int damage, GameObject damageSource)
    {
        if (!isActiveAndEnabled) return;

        onTakesHitEvent?.Invoke(damageSource);
        _currentHealth -= damage;
        if (_currentHealth <= 0) Die();
    }

    private void Die()
    {
        foreach (var drop in drops)
        {
            if (UnityEngine.Random.Range(0f, 1f) <= drop.dropChance)
            {
                ObjectPoolManager.Spawn(drop.item, transform.position, Quaternion.identity);
            }
        }

        PlayerStatus.ChangeScore(scoreReward);
        ObjectPoolManager.Despawn(gameObject);
    }
}
