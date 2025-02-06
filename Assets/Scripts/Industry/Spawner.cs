using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public ScObjFood propertisObject;
    [SerializeField] string keyProduct;
    [SerializeField] float spawnTime;
    [SerializeField] List<GameObject> spawners;
    [HideInInspector]
    public int count;

    private List<GameObject> _things;
    private Inventory _inventory;
    private List<Inventory> _inventories = new List<Inventory>();
    private AudioSource _audioSource;

    private float _timer;
    private float _randomTime;

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
        if (other.GetComponent<Inventory>() != null)
        {
            EnterTriggerInventory(other);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (_inventory != null)
        {
            MovePrefabInInventory(other);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Inventory>() != null)
        {
            ExitTriggerInventory(other);
        }
    }

    private void StartValues()
    {
        _things = new List<GameObject>(spawners.Count);
        for (int i = 0; i < spawners.Count; i++)
            _things.Add(null);

        _randomTime = spawnTime * Random.Range(0f, 0.15f);
        _audioSource = GetComponent<AudioSource>();
    }

    private void EnterTriggerInventory(Collider other)
    {
        if (_inventories.Count == 0)
        {
            _inventory = other.GetComponent<Inventory>();
        }
        _inventories.Add(other.GetComponent<Inventory>());
    }

    private void ExitTriggerInventory(Collider other)
    {
        foreach (Inventory item in _inventories)
        {
            if (other.GetComponent<Inventory>() == item)
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

    private void MovePrefabInInventory(Collider other)
    {
        if (_inventory.isHelper)
        {
            if (_inventory.idProduct == propertisObject.id)
            {
                for (int i = 0; i < _inventory.thing.Count; i++)
                {
                    if (_inventory.thing[i] == null && count > 0)
                    {
                        for (int n = 0; n < _things.Count; n++)
                        {
                            if (_things[n] != null)
                            {
                                count--;
                                _inventory.count++;
                                var prefab = _things[n];
                                prefab.transform.SetParent(_inventory.transform.GetChild(0).GetChild(0).GetChild(0));
                                StartCoroutine(_inventory.PrefabAnimation(prefab, _inventory.spawners[i]));
                                _inventory.thing[i] = prefab;
                                _things[n] = null;
                                break;
                            }
                        }
                    }
                }
            }
        }
        else
        {
            for (int i = 0; i < _inventory.thing.Count; i++)
            {
                if (_inventory.thing[i] == null && count > 0)
                {
                    for (int n = 0; n < _things.Count; n++)
                    {
                        if (_things[n] != null)
                        {
                            count--;
                            _inventory.count++;
                            var prefab = _things[n];
                            prefab.transform.SetParent(_inventory.transform.GetChild(0).GetChild(0).GetChild(0));
                            StartCoroutine(_inventory.PrefabAnimation(prefab, _inventory.spawners[i]));
                            _inventory.thing[i] = prefab;
                            _things[n] = null;
                            break;
                        }
                    }
                }
            }
        }
    }

    private void SpawnObject()
    {
        if (count < spawners.Count)
        {
            _timer += Time.deltaTime;
        }

        if (_timer >= spawnTime + _randomTime)
        {
            var obj = PoolProducts.GetFromPool(keyProduct);
            obj.transform.SetParent(transform.GetChild(0));
            _audioSource.Play();

            for (int i = 0; i < _things.Count; i++)
            {
                if (_things[i] == null)
                {
                    obj.transform.position = spawners[i].transform.position;
                    _things[i] = obj;
                    break;
                }
            }

            _randomTime = spawnTime * Random.Range(0f, 0.15f);
            _timer = 0;
            count++;
        }
    }
}
