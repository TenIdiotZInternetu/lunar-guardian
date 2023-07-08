using System.Collections.Generic;
using MovementPatterns;
using Spawnables;
using UnityEngine;

public class ObjectPoolManager : MonoBehaviour
{
    public static ObjectPoolManager Instance;
    public GameObject Container;
    public List<ObjectPool> objectPools;
    
    private static readonly Dictionary<string, ObjectPool> PoolTable = new();

    private void Start()
    {
        Instance = this;
        
        foreach (var pool in objectPools)
        {
            PoolTable.Add(pool.Key, pool);
            
            for (int i = 0 ; i < pool.initialPoolSize ; i++)
            {
                GameObject obj = Instantiate(pool.prefab, Container.transform, false);
                obj.name = pool.Key;
                pool.Enqueue(obj);
                obj.SetActive(false);
            }
        }
    }
    
    public static GameObject Spawn(GameObject prefab, Vector3 position, Quaternion rotation)
    {
        Entity prefabScript = prefab.GetComponent<Entity>();
        
        if (prefabScript == null)
        {
            Debug.LogError($"{prefab.name} is not a spawnable Entity");
            return null;
        }
        
        string prefabType = prefabScript.GetType().Name;
        GameObject obj = PoolTable[prefabType].Extract();
        
        obj.transform.position = position;
        obj.transform.rotation = rotation;
        
        Entity objScript = obj.GetComponent<Entity>();
        objScript.MovementPattern = prefab.GetComponent<MovementPattern>();

        obj.SetActive(true);
        return obj;
    }

    public static GameObject Spawn(GameObject prefab, Vector3 position, float rotation)
    {
        return Spawn(prefab, position, Quaternion.Euler(0, 0, rotation));
    }

    public static GameObject Despawn(GameObject obj)
    {
        obj.SetActive(false);
        PoolTable[obj.name].Enqueue(obj);
        
        return obj;
    }
}