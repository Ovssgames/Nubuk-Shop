using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SellShalf : MonoBehaviour
{
    public ScObjFood type;
    public List<GameObject> invent;
    public List<GameObject> cells;

    [HideInInspector]
    public int count;
    [HideInInspector]
    public int _maxCells;

    private void Start()
    {
        _maxCells = cells.Count;
    }

    private void OnTriggerEnter(Collider other)
    {
        MoveToShalfSell(other);
    }

    private void MoveToShalfSell(Collider other)
    {
        if (other.GetComponent<Inventory>() != null)
        {
            var inventory = other.GetComponent<Inventory>();
            var things = inventory.thing;
            for (int i = 0; i < things.Count && count < _maxCells; i++)
            {
                if (things[i] != null && inventory.thing[i].GetComponent<PrefabProperty>().propertisObject == type)
                {
                    for (int n = 0; n < cells.Count; n++)
                    {
                        if (invent[n] == null)
                        {
                            count++;
                            inventory.count--;
                            StartCoroutine(inventory.PrefabAnimation(things[i], cells[n]));
                            invent[n] = things[i];
                            things[i].transform.SetParent(transform.GetChild(0));
                            break;
                        }
                    }
                    things[i] = null;
                }
            }
        }
        else if (other.GetComponent<BuyerInventory>() != null)
        {
            var inventory = other.GetComponent<BuyerInventory>();
            if (inventory.idProduct == type.id)
            {
                for (int i = 0; i < inventory.thing.Count; i++)
                {
                    if (inventory.thing[i] == null && count > 0)
                    {
                        for (int n = 0; n < invent.Count; n++)
                        {
                            if (invent[n] != null)
                            {
                                count--;
                                inventory.count++;
                                var prefab = invent[n];
                                prefab.transform.SetParent(inventory.transform.GetChild(0).GetChild(0).GetChild(0));
                                StartCoroutine(inventory.PrefabAnimation(prefab, inventory.spawners[i]));
                                inventory.thing[i] = prefab;
                                invent[n] = null;
                                break;
                            }
                        }
                    }
                }
            }
        }
    }
}
