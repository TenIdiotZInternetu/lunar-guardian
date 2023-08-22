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
    
    private const string PLAYER_PROJECTILE_TAG = "PlayerProjectile";
    private const string BORDER_TAG = "PlayfieldBorder";
    
    
    public List<GameObject> shootableSources;
    public int maxHealth;

    public int scoreReward;
    public List<LootDrop> drops;

    public event Action ShootsEvent;
    
    [SerializeField] private GameObjectEvent onTakesHitEvent;

    private bool _hasAggro = false;
    private int _currentHealth;

    void Start()
    {
        _currentHealth = maxHealth;
        
        foreach (var spawner in shootableSources)
        {
            var spawnerScript = spawner.GetComponent<IShootable>();
            ShootsEvent += spawnerScript.OnShoot;
        }
        
        BombController.OnBombDamageTick += (damage) => TakeDamage(damage, Player.Instance.gameObject);
    }
    
    void Update()
    {
        base.Update();
        if (_hasAggro) ShootsEvent?.Invoke();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        GameObject collidedObject = other.gameObject;
        if (collidedObject.CompareTag(PLAYER_PROJECTILE_TAG))
        {
            OnProjectileCollision(collidedObject);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        GameObject collidedObject = other.gameObject;
        if (collidedObject.CompareTag(BORDER_TAG)) SwitchAggro();
    }

    private void OnProjectileCollision(GameObject projectile)
    {
        var projectileScript = projectile.GetComponent<Projectile>();
        TakeDamage(projectileScript.Damage, projectile);
        ObjectPoolManager.Despawn(projectile);
    }

    private void SwitchAggro()
    {
        _hasAggro = !_hasAggro;
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
        DropLoot();
        PlayerStatus.ChangeScore(scoreReward);
        ObjectPoolManager.Despawn(gameObject);
    }

    private void DropLoot()
    {
        foreach (var drop in drops)
        {
            float roll = UnityEngine.Random.Range(0f, 1f);
            if ( roll <= drop.dropChance)
            {
                Vector3 posOffset = new Vector3(roll, 1 - roll);
                ObjectPoolManager.Spawn(drop.item, transform.position + posOffset, Quaternion.identity);
                return;
            }
        }
    }
}
