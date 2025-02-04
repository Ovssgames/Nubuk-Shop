using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public float speedMove;
    public Animator animator;

    [HideInInspector]
    public int count;
    [HideInInspector]
    public int idProduct;

    public List<GameObject> thing;
    public List<GameObject> spawners;

    public bool isHelper;

    public IEnumerator PrefabAnimation(GameObject prefab, GameObject finish)
    {
        var distanse = Vector3.Distance(prefab.transform.position, finish.transform.position);
        if (count == 0)
        {
            animator.SetBool("IsHand", false);
            animator.SetLayerWeight(1, 0);
        }
        else
        {
            animator.SetBool("IsHand", true);
            animator.SetLayerWeight(1, 1);
        }

        while (distanse > 0.01f)
        {
            distanse = Vector3.Distance(prefab.transform.position, finish.transform.position);
            prefab.transform.position = Vector3.MoveTowards(prefab.transform.position, finish.transform.position, Time.deltaTime * speedMove);
            yield return null;
        }

        prefab.transform.rotation = finish.transform.rotation;
        prefab.transform.position = finish.transform.position;
        yield break;
    }
}
