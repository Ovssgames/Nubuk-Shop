using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashCan : MonoBehaviour
{
    [SerializeField] List<GameObject> animPosition;
    private void OnTriggerEnter(Collider other)
    {
        var inventory = other.GetComponent<Inventory>();
        if (inventory != null)
        {
            StartCoroutine(DestroyObjects(inventory));
        }
    }
    
    private IEnumerator DestroyObjects(Inventory inv)
    {
        for (int i = 0; i < inv.thing.Count; i++)
        {
            if (inv.thing[i] != null)
            {
                inv.thing[i].transform.SetParent(null);
                yield return StartCoroutine(inv.PrefabAnimation(inv.thing[i], animPosition[0]));
                yield return StartCoroutine(inv.PrefabAnimation(inv.thing[i], animPosition[1]));
                Destroy(inv.thing[i]);
                yield return null;
            }
        }
    }
}
