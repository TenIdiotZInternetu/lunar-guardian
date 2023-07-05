using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

[Serializable]
public class ObjectPool
{
    public GameObject prefab;
    public int initialPoolSize;
    public int maxPoolSize;
    
    public int poolSize => pool.Count;
    
    private Queue<GameObject> pool = new();
    
    public void Enqueue(GameObject obj) => pool.Enqueue(obj);
    
    public GameObject Extract()
    {
        GameObject obj;

        obj = pool.Count == 0 ?
            Object.Instantiate(prefab) : pool.Dequeue();
        
        return obj;
    }
}