using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyerInventory : MonoBehaviour
{
    public float speedMove;
    [SerializeField] float timeToFinish;

    [HideInInspector]
    public int count;
    [HideInInspector]
    public int idProduct;

    public List<GameObject> thing;
    public List<GameObject> spawners;

    public BuyerController buyerController;

    public IEnumerator PrefabAnimationHelper(GameObject prefab, GameObject finish)
    {
        float timer = 0;

        while (prefab.transform.position != finish.transform.position && timer < timeToFinish)
        {
            timer += Time.deltaTime;
            prefab.transform.position = Vector3.MoveTowards(prefab.transform.position, finish.transform.position, Time.deltaTime * speedMove);
            yield return null;
        }
        yield return null;
        prefab.transform.rotation = finish.transform.rotation;
        prefab.transform.localPosition = Vector3.zero;
    }
}
