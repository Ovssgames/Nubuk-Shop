using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SellShalf : MonoBehaviour
{
    public List<GameObject> sellObject;
    public List<Transform> cells;

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
                if (things[i] != null)
                {
                    _count++;
                    for (int n = 0; cells[n] == null; n++)
                        sellObject[n] = things[i];
                    things[i] = null;
                }
            }
        }
    }

}
