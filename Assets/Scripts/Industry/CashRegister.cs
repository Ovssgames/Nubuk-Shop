using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CashRegister : MonoBehaviour
{
    public List<Prise> products;
    public List<Transform> queueBuyers;

    [HideInInspector]
    public List<GameObject> buyers = new List<GameObject>();

    private bool _isWorking;
    private bool _isTrigger;

    private void Start()
    {
        StartValues();
    }

    private void Update()
    {
        if (!_isWorking && CashRegisterBuyer.isTrigger && _isTrigger)
        {
            Debug.Log("Cash");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerController>() != null)
        {
            _isTrigger = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<PlayerController>() != null)
        {
            _isTrigger = false;
        }
    }

    private void StartValues()
    {
        for (int i = 0; i < queueBuyers.Count; i++)
            buyers.Add(null);
    }
}

[System.Serializable]
public class Prise
{
    public ScObjFood product;
    public int prise;
}