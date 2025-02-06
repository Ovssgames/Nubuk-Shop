using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashCan : MonoBehaviour
{
    [SerializeField] List<GameObject> animPosition;
    private AudioSource _audioSourse;

    private void Start()
    {
        _audioSourse = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        var inventory = other.GetComponent<Inventory>();
        
        if (inventory != null)
        {
            for (int i = 0; i < inventory.thing.Count; i++)
            {
                if (inventory.thing[i] != null)
                {
                    StartCoroutine(DestroyObjects(inventory, i));
                    inventory.count--;
                }
            }
            _audioSourse.Play();
        }
    }
    
    private IEnumerator DestroyObjects(Inventory inv, int index)
    {
        yield return StartCoroutine(inv.PrefabAnimation(inv.thing[index], animPosition[0]));
        yield return StartCoroutine(inv.PrefabAnimation(inv.thing[index], animPosition[1]));
        Destroy(inv.thing[index]);
        inv.thing[index] = null;
        yield break;
    }
}
