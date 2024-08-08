using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manufacture : MonoBehaviour
{
    [SerializeField] float timeToMaking;
    [SerializeField] int countForMashineStart;
    [SerializeField] GameObject animationPoint;

    [Header("Start")]
    [SerializeField] ScObjFood startType;
    public List<GameObject> startCells;
    
    [Header("Finish")]
    [SerializeField] ScObjFood finishType;
    public List<GameObject> finishCells;

    [HideInInspector]
    public int countFinish;
    [HideInInspector]
    public int countStart;
    [HideInInspector]
    public int maxCountStart;
    [HideInInspector]
    public int maxCountFinish;

    [HideInInspector]
    public List<GameObject> _startThings;
    [HideInInspector]
    public List<GameObject> _finishThings;

    private bool _isWorking = false;
    private bool _isTrigger = false;
    private Inventory _inventory;

    private void Start()
    {
        StartValues();
    }

    private void Update()
    {
        MakingThings();
    }

    private void OnTriggerEnter(Collider other)
    {
        _inventory = other.GetComponent<Inventory>();
        StartThingsForMashine(other);
        _isTrigger = true;
    }

    private void OnTriggerExit(Collider other)
    {
        _isTrigger = false;
    }

    private void MakingThings()
    {
        if (countStart >= countForMashineStart && !_isWorking && !_isTrigger && countFinish < maxCountFinish)
        {
            Debug.Log("StartMakingCoroutine");
            StartCoroutine(Making());
        }
    }

    private void StartValues()
    {
        maxCountStart = startCells.Count;
        _startThings = new List<GameObject>(startCells.Count);
        for (int i = 0; i < startCells.Count; i++)
        {
            _startThings.Add(null);
        }

        maxCountFinish = finishCells.Count;
        _finishThings = new List<GameObject>(finishCells.Count);
        for (int i = 0; i < finishCells.Count; i++)
        {
            _finishThings.Add(null);
        }
    }
    private void StartThingsForMashine(Collider other)
    {
        if (_inventory != null)
        {
            var things = _inventory.thing;
            for (int i = 0; i < things.Count && countStart < maxCountStart; i++)
            {
                if (things[i] != null && _inventory.thing[i].GetComponent<PrefabProperty>().propertisObject == startType)
                {
                    for (int n = 0; n < _startThings.Count; n++)
                    {
                        if (_startThings[n] == null)
                        {
                            countStart++;
                            _inventory.count--;
                            StartCoroutine(_inventory.PrefabAnimation(things[i], startCells[n]));
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

    private IEnumerator Making()
    {
        _isWorking = true;
        int counter = 0;
        List<GameObject> destroyedObj = new List<GameObject>();

        for (int i = 0; i < _startThings.Count && counter < countForMashineStart; i++)
        {
            if (_startThings[i] != null)
            {
                StartCoroutine(_inventory.PrefabAnimation(_startThings[i], animationPoint));
                countStart--;
                counter++;
                destroyedObj.Add(_startThings[i]); 
                Debug.Log("make");
                _startThings[i] = null;
                yield return null;
            }
        }
        yield return new WaitForSeconds(timeToMaking);

        foreach (GameObject item in destroyedObj)
        {
            Destroy(item);
        }

        var finishObj = Instantiate(finishType.model, animationPoint.transform.position, animationPoint.transform.rotation);
        
        finishObj.transform.SetParent(transform.GetChild(1));
        GameObject finishCell = null;

        for (int i = 0; i < _finishThings.Count; i++)
        {
            if (_finishThings[i] == null)
            {
                _finishThings[i] = finishObj;
                finishCell = finishCells[i];
                break;
            }
        }

        yield return StartCoroutine(_inventory.PrefabAnimation(finishObj, finishCell));

        _isWorking = false;
        counter = 0;
        countFinish++;

        yield break;
    }
}
