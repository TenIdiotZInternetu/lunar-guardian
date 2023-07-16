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

    protected event EventHandler ShootsEvent;
    private int _currentHealth;

    void Start()
    {
        foreach (var spawner in bulletSpawners)
        {
            ShootsEvent += spawner.Shoot;
        }
    }
    
    public void Update()
    {
        base.Update();
        ShootsEvent?.Invoke(this, null);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (!other.gameObject.CompareTag("PlayerProjectile")) return;
        
        var projectile = other.gameObject.GetComponent<Projectile>();
        _currentHealth -= (int) projectile.Damage;
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
