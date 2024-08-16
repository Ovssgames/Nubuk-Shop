using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CashRegisterBuyer : MonoBehaviour
{
    public bool isTrigger;
    public BuyerInventory buyerInventory;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<BuyerInventory>() != null)
        {
            isTrigger = true;
            buyerInventory = other.GetComponent<BuyerInventory>();
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
