using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public float speedMove;
    public List<GameObject> thing;
    public List<GameObject> spawners;

    public IEnumerator PrefabAnimation(GameObject prefab, GameObject finish)
    {
        while (prefab.transform.position != finish.transform.position)
        {
            prefab.transform.position = Vector3.MoveTowards(prefab.transform.position, finish.transform.position, Time.deltaTime * speedMove);
            yield return null;
        }
    }
}
