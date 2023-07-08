using System.Collections.Generic;
using UnityEngine;

public class EnemySet : MonoBehaviour
{
    public GameObject spawner;
    public List<GameObject> enemies = new();

    private int _index = 0;

    public void SpawnNext()
    {
        GameObject enemy = enemies[_index];
        Vector3 spawnPoint = spawner.transform.position;
        
        var spawnedEnemy = ObjectPoolManager.Spawn(enemy, spawnPoint, enemy.transform.rotation);
        _index++;
    }
}
