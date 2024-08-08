using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HelperController : MonoBehaviour
{
    [Range(0, 1)]
    [SerializeField] float partQuantity;
    [SerializeField] float timeToWait;
    [SerializeField] float distanseForFinish;

    [SerializeField] Transform _targetPosition;
    [Header("Objects")]
    [SerializeField] Transform trashCan;
    [SerializeField] List<RouteSell> route;
    [Header("nav Mesh Agent")]
    [SerializeField] NavMeshAgent navMeshAgent;

    private bool _isWorking = false;
    private bool _isFind = false;

    private Transform _startPosition;
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
        _inventory = GetComponent<Inventory>();
        Physics.IgnoreCollision(GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterController>(), GetComponent<Collider>());
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
        }
        yield return null;


        while (_inventory.count != _inventory.thing.Count)
        {
            yield return null;
        }
        _targetPosition.position = route[_index].finishPosition.position;

        yield return null;

        float finPos = Vector3.Distance(_targetPosition.position, transform.position);
        while (finPos > distanseForFinish && _inventory.count == _inventory.thing.Count)
        {
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
            if (route[i].finishPosition.GetComponentInParent<SellShalf>() != null)
            {
                var shalf = route[i].finishPosition.GetComponentInParent<SellShalf>();

                _startPosition = route[i].startPosition[(int)UnityEngine.Random.Range(0, route[i].startPosition.Count)];
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
                if (route[i].finishPosition.GetComponentInParent<Manufacture>() != null)
                {
                    var manufacture = route[i].finishPosition.GetComponentInParent<Manufacture>();
                    if (manufacture.countStart == 0)
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
                    var shalf = route[i].finishPosition.GetComponentInParent<SellShalf>();
                    if (shalf.count <= shalf._maxCells)
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
    public int id;
    public List<Transform> startPosition;
    public Transform finishPosition;
    public bool isSpawner;
}