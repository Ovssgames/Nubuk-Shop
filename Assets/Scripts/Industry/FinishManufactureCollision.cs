using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishManufactureCollision : Manufacture
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("triggerFinishManufacture");
        Inventory inventory = other.GetComponent<Inventory>();
        for (int i = 0; i < inventory.thing.Count; i++)
        {
            if (inventory.thing[i] == null && countFinish > 0)
            {
                for (int n = 0; n < _finishThings.Count; n++)
                {
                    if (_finishThings[n] != null)
                    {
                        countFinish--;
                        inventory.count++;
                        var prefab = _finishThings[n];
                        prefab.transform.SetParent(inventory.transform.GetChild(0).GetChild(0).GetChild(0));
                        StartCoroutine(inventory.PrefabAnimation(prefab, inventory.spawners[i]));
                        inventory.thing[i] = prefab;
                        _finishThings[n] = null;
                        break;
                    }
                }
            }
        }
    }
}
