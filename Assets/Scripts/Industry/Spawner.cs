using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] ScObjFood propertisObject;
    [SerializeField] float spawnTime;
    [SerializeField] List<GameObject> spawners;

    private List<GameObject> _things;

    private float _timer;
    private int _count;

    private void Start()
    {
        StartValues();
    }

    private void Update()
    {
        SpawnObject();
    }

    private void OnTriggerEnter(Collider other)
    {
        MovePrefabInInventory(other);
    }

    private void StartValues()
    {
        _things = new List<GameObject>(spawners.Count);
        for (int i = 0; i < spawners.Count; i++)
            _things.Add(null);
    }

    private void MovePrefabInInventory(Collider other)
    {
        Inventory inventory = other.GetComponent<Inventory>();

        for (int i = 0; i < inventory.thing.Count; i++)
        {
            if (inventory.thing[i] == null && _count > 0)
            {
                for (int n = 0; n < _things.Count; n++)
                {
                    if (_things[n] != null)
                    {
                        _count--;
                        var prefab = _things[n];
                        prefab.transform.SetParent(inventory.transform.GetChild(0).GetChild(0).GetChild(0));
                        StartCoroutine(inventory.PrefabAnimation(prefab, inventory.spawners[i]));
                        inventory.thing[i] = prefab;
                        _things[n] = null;
                        break;
                    }
                }
            }
        }
    }

    private void SpawnObject()
    {
        if (_count < spawners.Count)
        {
            _timer += Time.deltaTime;
        }

        if (_timer >= spawnTime)
        {
            var obj = Instantiate(propertisObject.model);
            obj.transform.SetParent(transform.GetChild(0));

            for (int i = 0; i < _things.Count; i++)
            {
                if (_things[i] == null)
                {
                    obj.transform.position = spawners[i].transform.position;
                    _things[i] = obj;
                    break;
                }
            }

            _timer = 0;
            _count++;
        }
    }
}
