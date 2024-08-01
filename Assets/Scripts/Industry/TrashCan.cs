using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashCan : MonoBehaviour
{
    [SerializeField] List<GameObject> animPosition;
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("trigger");
        var inventory = other.GetComponent<Inventory>();
        if (inventory != null)
        {
            Debug.Log("мусорќ„ ј");
            StartCoroutine(DestroyObjects(inventory));
        }
    }
    
    private IEnumerator DestroyObjects(Inventory inv)
    {
        for (int i = 0; i < inv.thing.Count; i++)
        {
            inv.thing[i].transform.SetParent(null);
            Debug.Log("1 варик");
            yield return StartCoroutine(inv.PrefabAnimation(inv.thing[i], animPosition[0]));
            Debug.Log("2 варик");
            yield return StartCoroutine(inv.PrefabAnimation(inv.thing[i], animPosition[1]));
            Debug.Log("3 варик");
            Destroy(inv.thing[i]);
            yield return null;
        }
    }


}
