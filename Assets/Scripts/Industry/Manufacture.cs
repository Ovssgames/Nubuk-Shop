using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

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
    public UnityEvent onFinishCollision;


    private int _countStart;
    private int _maxCountStart;

    private int _countFinish;
    private int _maxCountFinish;
    
    private List<GameObject> _startThings;
    private List<GameObject> _finishThings;

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
        StartThingsForMashine(other);
        _isTrigger = true;
    }

    private void OnTriggerExit(Collider other)
    {
        _isTrigger = false;
    }

    private void MakingThings()
    {
        if (_countStart >= countForMashineStart && !_isWorking && !_isTrigger && _countFinish < _maxCountFinish)
        {
            Debug.Log("StartMakingCoroutine");
            StartCoroutine(Making());
        }
    }

    private void StartValues()
    {
        _inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
        _maxCountStart = startCells.Count;
        _startThings = new List<GameObject>(startCells.Count);
        for (int i = 0; i < startCells.Count; i++)
        {
            _startThings.Add(null);
        }

        _maxCountFinish = finishCells.Count;
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

    public void MovePrefabInInventory(Inventory inventory)
    {
        for (int i = 0; i < inventory.thing.Count; i++)
        {
            if (inventory.thing[i] == null && _countFinish > 0)
            {
                for (int n = 0; n < _finishThings.Count; n++)
                {
                    if (_finishThings[n] != null)
                    {
                        _countFinish--;
                        var prefab = _finishThings[n];
                        prefab.transform.SetParent(inventory.transform.GetChild(0).GetChild(0).GetChild(0));
                        StartCoroutine(inventory.PrefabAnimation(prefab, inventory.spawners[i]));
                        inventory.thing[i] = prefab;
                        _finishThings[n] = null;
                    }
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
                _countStart--;
                yield return StartCoroutine(_inventory.PrefabAnimation(_startThings[i], animationPoint));
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
        _countFinish++;

        yield break;
    }
}
