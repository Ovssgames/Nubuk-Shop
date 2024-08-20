using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CashRegisterBuyer : MonoBehaviour
{
    [HideInInspector]
    public bool isTrigger;
    [HideInInspector]
    public BuyerInventory buyerInventory;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<BuyerInventory>() != null && buyerInventory == null)
        {
            isTrigger = true;
            buyerInventory = other.GetComponent<BuyerInventory>();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<BuyerInventory>() == buyerInventory)
        {
            isTrigger = false;
            buyerInventory = null;
        }
    }
}
