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

    [SerializeField] Transform _targetPosition;

    [SerializeField] List<RouteSell> route;

    [SerializeField] NavMeshAgent navMeshAgent;

    private bool _isWorking = false;
    private bool _isFind = false;
    private bool _isTimePassed = false;

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


        Debug.Log(_targetPosition.position);
    }

    private void StartValue()
    {
        _inventory = GetComponent<Inventory>();
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
            SecondSelection();
            yield return null;
        }

        if (!_isFind)
        {
            StopCoroutine(AIRoute());
            _isWorking = false;
        }
        yield return null;

        Invoke("TimePassed", timeToWait);

        Vector3 finpos = new Vector3(_targetPosition.position.x, transform.position.y, _targetPosition.position.z);
        while (_inventory.count != _inventory.thing.Count && !_isTimePassed)
        {
            yield return null;
        }

        _targetPosition.position = route[_index].finishPosition.position;
    }

    private void FirstSelection()
    {
        for (int i = 0; i < route.Count; i++)
        {
            var shalf = route[i].finishPosition.GetComponentInParent<SellShalf>();
            
            _startPosition = route[i].startPosition[(int)UnityEngine.Random.Range(0, route[i].startPosition.Count)];
            if (route[i].isSpawner)
            {
                if (shalf.count <= shalf._maxCells * partQuantity)
                {
                    _targetPosition.position = _startPosition.position;
                    _isFind = true;
                    _index = i;
                    break;
                }
            }
            else
            {
                var manufacture = _startPosition.GetComponentInParent<Manufacture>();
                if (shalf.count <= shalf._maxCells * partQuantity && manufacture.countFinish >= manufacture._maxCountFinish / 2)
                {
                    _targetPosition.position = _startPosition.position;
                    _isFind = true;
                    _index = i;
                    break;
                }
            }
        }
    }

    private void SecondSelection()
    {

    }

    private void TimePassed()
    {
        _isTimePassed = true;
    }

}

[Serializable]
public class RouteSell
{
    public List<Transform> startPosition;
    public Transform finishPosition;
    public bool isSpawner;
}