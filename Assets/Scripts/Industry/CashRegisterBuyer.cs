using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CashRegisterBuyer : MonoBehaviour
{
    public bool isTrigger;


    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<BuyerInventory>() != null)
        {
            isTrigger = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<BuyerInventory>() != null)
        {
            isTrigger = false;
        }
    }
}
