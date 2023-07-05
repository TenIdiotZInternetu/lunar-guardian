using System;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolManager : MonoBehaviour
{
    public static ObjectPoolManager Instance;
    public List<ObjectPool> objectPools;
    
    private static Dictionary<string, ObjectPool> _poolTable = new();

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
    
    public static GameObject Spawn(GameObject prefab, Vector3 position, Quaternion rotation)
    {
        GameObject obj = _poolTable[prefab.name].Extract();
        obj.transform.position = position;
        obj.transform.rotation = rotation;
        return obj;
    }

    public static GameObject Spawn(GameObject prefab, Vector3 position, float rotation)
    {
        return Spawn(prefab, position, Quaternion.Euler(0, 0, rotation));
    }

}