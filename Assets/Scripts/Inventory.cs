using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public float speedMove;

    [HideInInspector]
    public int count;
    [HideInInspector]
    public int idProduct;

    public List<GameObject> thing;
    public List<GameObject> spawners;

    public bool isHelper;

    public IEnumerator PrefabAnimation(GameObject prefab, GameObject finish)
    {
        while (prefab.transform.position != finish.transform.position)
        {
            prefab.transform.position = Vector3.MoveTowards(prefab.transform.position, finish.transform.position, Time.deltaTime * speedMove);
            yield return null;
        }
        prefab.transform.rotation = finish.transform.rotation;
    }
}
