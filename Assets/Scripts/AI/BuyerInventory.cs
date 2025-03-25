using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyerInventory : MonoBehaviour
{
    public float speedMove;
    [SerializeField] Animator animator;

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
        var distanse = Vector3.Distance(prefab.transform.position, finish.transform.position);


        while (distanse > 0.01f && timer <= 4f)
        {
            distanse = Vector3.Distance(prefab.transform.position, finish.transform.position);
            timer += Time.deltaTime;
            prefab.transform.position = Vector3.MoveTowards(prefab.transform.position, finish.transform.position, Time.deltaTime * speedMove);
            yield return null;
        }
        prefab.transform.rotation = finish.transform.rotation;
        prefab.transform.position = finish.transform.position;
        yield break;
    }
}
