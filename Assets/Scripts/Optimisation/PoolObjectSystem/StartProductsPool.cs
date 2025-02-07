using System.Collections.Generic;
using UnityEngine;

public class StartProductsPool : MonoBehaviour
{
    [SerializeField] List<ProductPool> productPoolObject;

    private void Awake()
    {
        foreach (ProductPool item in productPoolObject)
        {
            PoolProducts.CreatePool(item.productKey, item.productPrefab, item.initialSize, item.maxSize);
        }
    }
}

[System.Serializable]
public class ProductPool
{
    public string productKey;
    public GameObject productPrefab;
    public int initialSize = 10;
    public int maxSize = 50;
}