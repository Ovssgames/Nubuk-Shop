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
        while (prefab.transform.position != finish.transform.position && prefab.transform.rotation.y != finish.transform.rotation.y)
        {
            prefab.transform.position = Vector3.MoveTowards(prefab.transform.position, finish.transform.position, Time.deltaTime * speedMove);
            prefab.transform.rotation = Quaternion.Lerp(prefab.transform.rotation, finish.transform.rotation, Time.deltaTime * speedMove);
            yield return null;
        }
        prefab.transform.rotation = finish.transform.rotation;
        Debug.Log("End Position");
    }
}
