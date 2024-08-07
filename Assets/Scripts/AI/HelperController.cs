using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HelperController : MonoBehaviour
{
    [Range(0, 1)]
    [SerializeField] float partQuantity; 

    [SerializeField] List<RouteSell> route;

    [SerializeField] NavMeshAgent navMeshAgent;

    private bool _isWorking = false;
    private bool _isFind = false;

    private Transform _startPosition;
    private Transform _targetPosition;
    private Inventory _inventory;

    private void Start()
    {
        StartValues();
    }


    private void Update()
    {
        if (!_isWorking)
            StartCoroutine(AIRoute());

        navMeshAgent.destination = _targetPosition.position;
    }

    private void StartValues()
    {
    }

    private IEnumerator AIRoute()
    {
        _isWorking = true;
        FirstSelection();

        yield return null;
        
        if (_isFind)
        {
            yield return null;
        }
        else
        {
            StopCoroutine(AIRoute());
        }

        if (_inventory.count == _inventory.thing.Count)
        {
            yield return null;
        }


    }

    private void FirstSelection()
    {
        for (int i = 0; i < route.Count; i++)
        {
            var shalf = route[i].finishPosition.GetComponentInParent<SellShalf>();
            var manufacture = _startPosition.gameObject.GetComponentInParent<Manufacture>();
            
            _startPosition = route[i].startPosition[(int)UnityEngine.Random.Range(0, route[i].startPosition.Count)];

            if (shalf.count <= shalf._maxCells * partQuantity && (_startPosition.gameObject.GetComponentInParent<Spawner>() != null || manufacture.countFinish >= manufacture._maxCountFinish / 2))
            {
                _targetPosition.position = _startPosition.position;
                _isFind = true;
                break;
            }
        }
    }
}

[Serializable]
public class RouteSell
{
    public List<Transform> startPosition;
    public Transform finishPosition;
    public bool isSpawner;
}