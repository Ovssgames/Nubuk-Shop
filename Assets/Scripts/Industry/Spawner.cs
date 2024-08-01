using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] ScObjFood propertisObject;
    [SerializeField] float spawnTime;
    [SerializeField] List<GameObject> spawners;

    private float _timer;
    private int _count;

    private void Update()
    {
        SpawnObject();
    }

    private void OnTriggerEnter(Collider other)
    {
        MovePrefabInInventory(other);
    }

    private void MovePrefabInInventory(Collider other)
    {
        var inventory = other.GetComponent<Inventory>();

        if (other.GetComponent<PlayerController>() != null)
        {
            for (int i = 0; i < inventory.thing.Count; i++)
            {
                if (inventory.thing[i] == null && _count > 0)
                {
                    Debug.Log("normul");
                    _count--;
                    var prefab = GetComponentInChildren<PrefabProperty>();
                    prefab.transform.SetParent(other.transform.GetChild(0).GetChild(0).GetChild(0));
                    StartCoroutine(inventory.PrefabAnimation(prefab.gameObject, inventory.spawners[i]));
                    inventory.thing[i] = prefab.gameObject;

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
            obj.transform.position = spawners[_count].transform.position;
            obj.transform.SetParent(transform.GetChild(0));
            _timer = 0;
            _count++;
        }
    }
}
