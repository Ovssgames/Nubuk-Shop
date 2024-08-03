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


    private int _countStart;
    private int _maxCountStart;
    private bool _isWorking = false;
    
    private List<GameObject> _startThings;
    private List<GameObject> _finishThings;

    private Inventory _inventory;

    private void Start()
    {
        StartSystemValues();
    }

    private void Update()
    {
        MakingThings();
    }

    private void OnTriggerEnter(Collider other)
    {
        StartThingsForMashine(other);
    }

    private void MakingThings()
    {
        if (_countStart >= countForMashineStart && !_isWorking)
        {
            StartCoroutine(Making());
        }
    }

    private void StartSystemValues()
    {
        _inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
        _maxCountStart = startCells.Count;
        _startThings = new List<GameObject>(startCells.Count);
        for (int i = 0; i < startCells.Count; i++)
        {
            _startThings.Add(null);
        }
    }
    private void StartThingsForMashine(Collider other)
    {
        if (_inventory != null)
        {
            var things = _inventory.thing;
            for (int i = 0; i < things.Count && _countStart < _maxCountStart; i++)
            {
                if (things[i] != null && _inventory.thing[i].GetComponent<PrefabProperty>().propertisObject == startType)
                {
                    _countStart++;
                    for (int n = 0; n < _startThings.Count; n++)
                    {
                        if (_startThings[n] == null)
                        {
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
        List<GameObject> destroyedObj = new List<GameObject>(counter);

        for (int i = 0; i < _startThings.Count && counter < countForMashineStart; i++)
        {
            if (_startThings[i] != null)
            {
                _countStart--;
                counter++;
                StartCoroutine(_inventory.PrefabAnimation(_startThings[i], animationPoint));
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

        Instantiate(finishType.model, animationPoint.transform.position, animationPoint.transform.rotation);
        _isWorking = false;
        counter = 0;
    }
}
