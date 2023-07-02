using System;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolManager : MonoBehaviour
{
    public static ObjectPoolManager Instance;
    public List<ObjectPool> objectPools;
    
    private Dictionary<string, ObjectPool> _poolTable = new();

    private void Start()
    {
        Instance = this;
        
        foreach (var pool in objectPools)
        {
            _poolTable.Add(pool.prefab.name, pool);
            
            for (int i = 0 ; i < pool.initialPoolSize ; i++)
            {
                GameObject obj = Instantiate(pool.prefab);
                pool.Enqueue(obj);
            }
        }
    }
    
    public GameObject Spawn(GameObject prefab, Vector3 position, float rotation)
    {
        GameObject obj = _poolTable[prefab.name].Extract();
        obj.transform.position = position;
        obj.transform.rotation = Quaternion.Euler(0, 0, rotation);
        return obj;
    }
}