using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manufacture : MonoBehaviour
{
    [SerializeField] int countForMashineStart;
    [SerializeField] ScObjFood startType;
    [SerializeField] ScObjFood finishType;

    public List<GameObject> startCells;
    public List<GameObject> finishCells;

    private List<GameObject> _startThings = new List<GameObject>(4);
    private List<GameObject> _finishThings;

    private int _countStart;
    private int _maxCountStart;
    

    private void Start()
    {
        _maxCountStart = startCells.Count;
    }

    private void OnTriggerEnter(Collider other)
    {
        StartThingsForMashine(other);
    }

    private void StartThingsForMashine(Collider other)
    {
        var inventory = other.GetComponent<Inventory>();
        if (inventory != null)
        {
            Debug.Log("ManufactureLayerWork");
            var things = inventory.thing;
            for (int i = 0; i < things.Count && _countStart < _maxCountStart; i++)
            {
                if (things[i] != null && inventory.thing[i].GetComponent<PrefabProperty>().propertisObject == startType)
                {
                    _countStart++;
                    for (int n = 0; n < _startThings.Count; n++)
                    {
                        if (_startThings[n] == null)
                        {
                            StartCoroutine(inventory.PrefabAnimation(things[i], startCells[n]));
                            _startThings[n] = things[i];
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
