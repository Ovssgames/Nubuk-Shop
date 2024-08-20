using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CashRegister : MonoBehaviour
{
    public List<ScObjFood> products;
    public List<Transform> queueBuyers;
    [SerializeField] float serviseTime;
    [SerializeField] CashRegisterBuyer cashRegisterBuyer;

    public List<GameObject> buyers = new List<GameObject>();

    [SerializeField] MoneyAnimation moneyAnimation;

    private bool _isWorking;
    private bool _isTrigger;

    private void Start()
    {
        StartValues();
    }

    private void Update()
    {
        if (!_isWorking && cashRegisterBuyer.isTrigger && _isTrigger)
        {
            StartCoroutine(BuyProducts());
        }
    }
    private void StartValues()
    {
        for (int i = 0; i < queueBuyers.Count; i++)
            buyers.Add(null);
    }

    private IEnumerator BuyProducts()
    {
        Debug.Log("StartCashRegisterCoroutine");
        _isWorking = true;
        yield return new WaitForSeconds(serviseTime);

        if (_isTrigger)
        {
            var bueyrProducts = cashRegisterBuyer.buyerInventory.thing;
            int sumMoney = 0;

            for (int i = 0; i < bueyrProducts.Count; i++)
            {
                if (bueyrProducts[i] != null)
                {
                    foreach (var item in products)
                    {
                        if (item.id == bueyrProducts[i].GetComponent<PrefabProperty>().propertisObject.id)
                        {
                            sumMoney += item.prise;
                        }
                    }
                }
            }
            StartCoroutine(moneyAnimation.MoneyPlus(sumMoney));
            yield return null;

            BuyerController buyer = buyers[0].GetComponent<BuyerController>();
            buyer.targetPosition.position = buyer._exit[(int)Random.Range(0, buyer._exit.Count)].position;
            buyers.RemoveAt(0);
            buyers.Add(null);

            yield return new WaitForSeconds(0.8f);

            for (int i = 0; i < buyers.Count; i++)
            {
                if (buyers[i] == null)
                    break;
                else
                    buyers[i].GetComponent<BuyerController>().targetPosition.position = queueBuyers[i].position;

            }
        }

        _isWorking = false;
        yield break;
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
}