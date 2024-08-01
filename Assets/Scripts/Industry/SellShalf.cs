using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SellShalf : MonoBehaviour
{
    [SerializeField] ScObjFood type;
    public List<GameObject> invent;
    public List<GameObject> cells;

    private int _count;
    private int _maxCells;

    private void Start()
    {
        _maxCells = cells.Count;
    }

    private void OnTriggerEnter(Collider other)
    {
        var inventory = other.GetComponent<Inventory>();
        if (inventory != null)
        {
            var things = inventory.thing;
            for (int i = 0; i < things.Count && _count < _maxCells; i++)
            {
                if (things[i] != null && inventory.thing[i].GetComponent<PrefabProperty>().propertisObject == type)
                {
                    _count++;
                    for (int n = 0; n < cells.Count; n++)
                    {
                        if (invent[n] == null)
                        {
                            Debug.Log(n);
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
    }

}
