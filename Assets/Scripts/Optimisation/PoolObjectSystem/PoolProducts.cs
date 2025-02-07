using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public static class PoolProducts
{
    private static Dictionary<string, IObjectPool<GameObject>> pools = new Dictionary<string, IObjectPool<GameObject>>();

    public static void CreatePool(string key, GameObject prefab, int initialSize = 10, int maxSize = 50)
    {
        if (!pools.ContainsKey(key))
        {
            pools[key] = new ObjectPool<GameObject>(
                createFunc: () => Object.Instantiate(prefab),
                actionOnGet: obj => obj.SetActive(true),
                actionOnRelease: obj => obj.SetActive(false),
                actionOnDestroy: obj => Object.Destroy(obj),
                collectionCheck: false,
                defaultCapacity: initialSize,
                maxSize: maxSize
            );
        }
    }

    public static GameObject GetFromPool(string key)
    {
        if (pools.ContainsKey(key))
        {
            return pools[key].Get();
        }
        Debug.LogWarning($"Pool with key '{key}' does not exist!");
        return null;
    }

    public static void ReturnToPool(string key, GameObject obj)
    {
        if (pools.ContainsKey(key))
        {
            pools[key].Release(obj);
        }
        else
        {
            Debug.LogWarning($"Pool with key '{key}' does not exist!");
        }
    }
}
