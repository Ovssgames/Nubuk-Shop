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

    private BuyerInventory _inventory;
    private List<BuyerInventory> _inventories = new List<BuyerInventory>();

    private void Start()
    {
        _maxCells = cells.Count;
    }

    private void OnTriggerEnter(Collider other)
    {
        MoveToShalfSell(other);

        if (other.GetComponent<BuyerInventory>() != null)
        {
            EnterTriggerInventory(other);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (_inventory != null)
        {
            MoveToBuyerInventory(other);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<BuyerInventory>() != null)
        {
            ExitTriggerInventory(other);
        }
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
    }

    private void MoveToBuyerInventory(Collider other)
    {
        if (_inventory.idProduct == type.id)
        {
            for (int i = 0; i < _inventory.thing.Count; i++)
            {
                var buyerController = _inventory.buyerController;
                if (_inventory.thing[i] == null && count > 0 && buyerController.countProduct<buyerController.countProductMax)
                {
                    for (int n = 0; n < invent.Count; n++)
                    {
                        if (invent[n] != null)
                        {
                            count--;
                            _inventory.buyerController.countProduct++;
                            _inventory.count++;
                            var prefab = invent[n];
                            prefab.transform.SetParent(_inventory.transform.GetChild(0).GetChild(0).GetChild(0));
                            StartCoroutine(_inventory.PrefabAnimation(prefab, _inventory.spawners[i]));
                            _inventory.thing[i] = prefab;
                            invent[n] = null;
                            break;
                        }
                    }
                }
            }
        }
    }

    private void EnterTriggerInventory(Collider other)
    {
        if (_inventories.Count == 0)
        {
            _inventory = other.GetComponent<BuyerInventory>();
        }
        _inventories.Add(other.GetComponent<BuyerInventory>());
    }

    private void ExitTriggerInventory(Collider other)
    {
        foreach (BuyerInventory item in _inventories)
        {
            if (other.GetComponent<BuyerInventory>() == item)
            {
                _inventories.Remove(item);
                break;
            }
        }

        if (_inventories.Count > 0)
        {
            _inventory = _inventories[0];
        }
        else
        {
            _inventory = null;
        }
    }
}
