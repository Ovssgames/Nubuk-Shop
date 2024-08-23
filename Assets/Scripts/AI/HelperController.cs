using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HelperController : MonoBehaviour
{
    [Range(0, 1)]
    [SerializeField] float partQuantity;
    [SerializeField] float distanseForFinish;
    [SerializeField] GameObject targetPositionPrefab;

    [Header("Objects")]
    [SerializeField] Transform trashCan;
    [SerializeField] List<RouteSell> route;
    [Header("Nav Mesh Agent")]
    [SerializeField] NavMeshAgent navMeshAgent;

    private bool _isWorking = false;
    private bool _isFind = false;

    private Transform _targetPosition;

    [HideInInspector]
    public Transform _startPosition;
    [HideInInspector]
    public Transform _finishPosition;

    private List<HelperController> _anothersHelpers= new List<HelperController>();

    private float _finishDistanse = 10000f;
    private float _startDistanse = 10000f;
    private Inventory _inventory;

    private void Awake()
    {
        AwakeValue();
    }

    private void Update()
    {
        if (!_isWorking)
        {
            StartCoroutine(AIRoute());
        }
        else
        {
            navMeshAgent.destination = _targetPosition.position;
        }
    }

    private void AwakeValue()
    {
        var tarPos = Instantiate(targetPositionPrefab);
        _targetPosition = tarPos.transform;

        _inventory = GetComponent<Inventory>();
        Physics.IgnoreCollision(GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterController>(), GetComponent<Collider>());

        foreach (RouteSell item in route)
        {
            if (item.startPosition[0].GetComponent<Spawner>() != null)
            {
                item.id = item.startPosition[0].GetComponent<Spawner>().propertisObject.id;
            }
            else
            {
                item.id = item.startPosition[0].GetComponentInParent<Manufacture>().finishType.id;
            }
        }

        GameObject[] helpers = GameObject.FindGameObjectsWithTag("Helper");
        for (int i = 0; i < helpers.Length; i++)
        {
            _anothersHelpers.Add(helpers[i].GetComponent<HelperController>());
        }

        navMeshAgent.avoidancePriority = 0;
    }

    private IEnumerator AIRoute()
    {
        Debug.Log("StartCoroutineBot");
        _isWorking = true;
        FirstSelection();

        yield return null;
        
        if (_isFind)
        {
            yield return null;
        }
        else
        {
            SecondSelection();
            yield return null;
        }

        if (!_isFind)
        {
            _isWorking = false;
            yield break;
        }
        yield return null;

        if (_startPosition.gameObject.GetComponent<Spawner>() != null)
        {
            while (_inventory.count != _inventory.thing.Count)
            {
                yield return null;
            }
            _targetPosition.position = _finishPosition.position;
        }
        else
        {
            _startDistanse = Vector3.Distance(_targetPosition.position, transform.position);
            while (_startDistanse > distanseForFinish)
            {
                _startDistanse = Vector3.Distance(_targetPosition.position, transform.position);
                yield return null;
            }
            _targetPosition.position = _finishPosition.position;
        }
        yield return null;

        _finishDistanse = Vector3.Distance(_targetPosition.position, transform.position);
        while (_finishDistanse > distanseForFinish)
        {
            _finishDistanse = Vector3.Distance(_targetPosition.position, transform.position);
            yield return null;
        }

        if (_inventory.count != 0)
        {
            _targetPosition.position = trashCan.position;

            while (_inventory.count != 0)
            {
                yield return null;
            }
        }

        _isWorking = false;
        _isFind = false;
    }

    private void FirstSelection()
    {
        for (int i = 0; i < route.Count; i++)
        {
            StartAndFinishFind(i);

            if (_finishPosition.GetComponentInParent<SellShalf>() != null && _startPosition.gameObject.activeSelf == true &&
                _finishPosition.gameObject.activeSelf == true)
            {
                var shalf = _finishPosition.GetComponentInParent<SellShalf>();

                if (route[i].isSpawner)
                {
                    if (shalf.count < shalf._maxCells * partQuantity)
                    {
                        _targetPosition.position = _startPosition.position;
                        _inventory.idProduct = route[i].id;
                        _isFind = true;
                        break;
                    }
                }
                else
                {
                    var manufacture = _startPosition.GetComponentInParent<Manufacture>();
                    if (shalf.count < shalf._maxCells * partQuantity && manufacture.countFinish >= manufacture.maxCountFinish / 2)
                    {
                        _targetPosition.position = _startPosition.position;
                        _inventory.idProduct = route[i].id;
                        _isFind = true;
                        break;
                    }
                }
            }
        }
    }

    private void SecondSelection()
    {
        for (int i = 0; i < route.Count; i++)
        {
            StartAndFinishFind(i);
            if (_startPosition.gameObject.activeSelf == true &&
                _finishPosition.gameObject.activeSelf == true)
            {
                if (route[i].isSpawner)
                {
                    if (route[i].finishPosition[0].GetComponentInParent<Manufacture>() != null)
                    {
                        var manufacture = _finishPosition.GetComponentInParent<Manufacture>();
                        if (manufacture.countStart < manufacture.maxCountStart)
                        {
                            _targetPosition.position = _startPosition.position;
                            _inventory.idProduct = route[i].id;
                            _isFind = true;
                            break;
                        }
                    }
                    else
                    {
                        var shalf = _finishPosition.GetComponentInParent<SellShalf>();
                        if (shalf.count < shalf._maxCells && shalf.count < shalf._maxCells)
                        {
                            _targetPosition.position = _startPosition.position;
                            _inventory.idProduct = route[i].id;
                            _isFind = true;
                            break;
                        }
                    }
                }
                else
                {
                    if (_finishPosition.GetComponentInParent<SellShalf>() != null)
                    {
                        var shalf = _finishPosition.GetComponentInParent<SellShalf>();
                        var manufacture = _startPosition.GetComponentInParent<Manufacture>();
                        if (shalf.count < shalf._maxCells && manufacture.countFinish > 0)
                        {
                            _targetPosition.position = _startPosition.position;
                            _inventory.idProduct = route[i].id;
                            _isFind = true;
                            break;
                        }
                    }
                    else
                    {
                        var manufactureStart = _startPosition.GetComponentInParent<Manufacture>();
                        var manufactureFinish = _finishPosition.GetComponentInParent<Manufacture>();
                        if (manufactureFinish.countStart < manufactureFinish.startCells.Count && manufactureStart.countFinish > 0)
                        {
                            _targetPosition.position = _startPosition.position;
                            _inventory.idProduct = route[i].id;
                            _isFind = true;
                            break;
                        }
                    }
                }
            }
        }
    }

    private void StartAndFinishFind(int index)
    {
        _startPosition = route[index].startPosition[(int)UnityEngine.Random.Range(0, route[index].startPosition.Count)];
        _finishPosition = route[index].finishPosition[(int)UnityEngine.Random.Range(0, route[index].finishPosition.Count)];

        if (_startPosition == null)
        {

        }
    }
}

[Serializable]
public class RouteSell
{
    [HideInInspector]
    public int id;
    public List<Transform> startPosition;
    public List<Transform> finishPosition;
    public bool isSpawner;
}