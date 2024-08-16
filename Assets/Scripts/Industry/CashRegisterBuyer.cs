using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CashRegisterBuyer : MonoBehaviour
{
    public bool isTrigger;
    public BuyerController buyerController;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<BuyerInventory>() != null)
        {
            isTrigger = true;
            buyerController = other.GetComponent<BuyerController>();
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
