using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using MovementPatterns;
using Spawnables;
using Spawnables.Projectiles;
using UnityEngine;

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
    public List<LootDrop> drops;

    public event EventHandler ShootsEvent;
    public event EventHandler<GameObject> GetsHitEvent; 

    private int _currentHealth;

    void Start()
    {
        _currentHealth = maxHealth;
        
        foreach (var spawner in bulletSpawners)
        {
            ShootsEvent += spawner.Shoot;
        }
    }
    
    void Update()
    {
        base.Update();
        ShootsEvent?.Invoke(this, null);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.gameObject.CompareTag("PlayerProjectile")) return;
        
        var projectile = other.gameObject.GetComponent<Projectile>();
        TakeDamage(projectile.Damage);
        
        GetsHitEvent?.Invoke(this, other.gameObject);
        ObjectPoolManager.Despawn(other.gameObject);
    }

    private void TakeDamage(int damage)
    {
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

        ObjectPoolManager.Despawn(gameObject);
    }
}
