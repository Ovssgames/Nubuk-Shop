using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CashRegister : MonoBehaviour
{
    public List<Prise> products;
    public List<Transform> queueBuyers;
    [SerializeField] float serviseTime;
    [SerializeField] CashRegisterBuyer cashRegisterBuyer;

    public List<GameObject> buyers = new List<GameObject>();

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
            BuyerController buyer = buyers[0].GetComponent<BuyerController>();
            buyer.targetPosition.position = buyer.exit[(int)Random.Range(0, buyer.exit.Count)].position;
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

[System.Serializable]
public class Prise
{
    public ScObjFood product;
    public int prise;
}