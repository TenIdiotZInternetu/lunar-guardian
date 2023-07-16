using System.Collections.Generic;
using MovementPatterns;
using Spawnables;
using UnityEngine;

public class ObjectPoolManager : MonoBehaviour
{
    public static ObjectPoolManager Instance;
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
                GameObject obj = Instantiate(pool.prefab, this.transform, false);
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
        GameObject spawnedObject = PoolTable[prefabType].Extract();
        
        spawnedObject.transform.position = position;
        spawnedObject.transform.rotation = rotation;
        
        Entity objScript = spawnedObject.GetComponent<Entity>();
        objScript.MovementPattern = prefab.GetComponent<MovementPattern>();

        spawnedObject.SetActive(true);
        return spawnedObject;
    }

    public static GameObject Spawn(GameObject prefab, Vector3 position, float rotationInAngles)
    {
        return Spawn(prefab, position, Quaternion.Euler(0, 0, rotationInAngles));
    }

    public static GameObject Despawn(GameObject obj)
    {
        obj.SetActive(false);
        
        string objType = obj.GetComponent<Entity>().GetType().Name;
        PoolTable[objType].Enqueue(obj);
        
        return obj;
    }
}