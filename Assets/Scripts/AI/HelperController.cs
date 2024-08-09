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

    private float _finishDistanse = 1000f;
    private Inventory _inventory;
    private int _index;

    private void Start()
    {
        StartValue();
    }

    private void Update()
    {
        if (!_isWorking)
            StartCoroutine(AIRoute());
        else
            navMeshAgent.destination = _targetPosition.position;
    }

    private void StartValue()
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
            StopCoroutine(AIRoute());
            _isWorking = false;
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

            while (_inventory.count == 0)
            {
                yield return null;
            }
            _targetPosition.position = _finishPosition.position;
        }
        yield return null;

        
        while (_finishDistanse > distanseForFinish && _inventory.count == _inventory.thing.Count)
        {
            _finishDistanse = Vector3.Distance(_targetPosition.position, transform.position);
            yield return null;
        }

        Debug.Log("Finished");
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
            if (route[i].finishPosition[0].GetComponentInParent<SellShalf>() != null)
            {
                var shalf = route[i].finishPosition[0].GetComponentInParent<SellShalf>();

                _startPosition = route[i].startPosition[(int)UnityEngine.Random.Range(0, route[i].startPosition.Count)];
                _finishPosition = route[i].finishPosition[(int)UnityEngine.Random.Range(0, route[i].finishPosition.Count)];
                if (route[i].isSpawner)
                {
                    if (shalf.count <= shalf._maxCells * partQuantity)
                    {
                        _targetPosition.position = _startPosition.position;
                        _inventory.idProduct = route[i].id;
                        _isFind = true;
                        _index = i;
                        break;
                    }
                }
                else
                {
                    var manufacture = _startPosition.GetComponentInParent<Manufacture>();
                    if (shalf.count <= shalf._maxCells * partQuantity && manufacture.countFinish >= manufacture.maxCountFinish / 2)
                    {
                        _targetPosition.position = _startPosition.position;
                        _inventory.idProduct = route[i].id;
                        _isFind = true;
                        _index = i;
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
            if (route[i].isSpawner)
            {
                _startPosition = route[i].startPosition[(int)UnityEngine.Random.Range(0, route[i].startPosition.Count)];
                _finishPosition = route[i].finishPosition[(int)UnityEngine.Random.Range(0, route[i].finishPosition.Count)];
                if (route[i].finishPosition[0].GetComponentInParent<Manufacture>() != null)
                {
                    var manufacture = _finishPosition.GetComponentInParent<Manufacture>();
                    if (manufacture.countStart == 0 && manufacture.countStart < manufacture.maxCountStart)
                    {
                        _targetPosition.position = _startPosition.position;
                        _inventory.idProduct = route[i].id;
                        _isFind = true;
                        _index = i;
                        break;
                    }
                }
                else
                {
                    var shalf = route[i].finishPosition[0].GetComponentInParent<SellShalf>();
                    if (shalf.count <= shalf._maxCells && shalf.count < shalf._maxCells)
                    {
                        _targetPosition.position = _startPosition.position;
                        _inventory.idProduct = route[i].id;
                        _isFind = true;
                        _index = i;
                        break;
                    }
                }
            }
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