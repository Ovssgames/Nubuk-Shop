using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class OpenIndustryItem : MonoBehaviour
{
    [SerializeField] type typeIndustry;
    [SerializeField] int prise;
    [SerializeField] float timeToBuy;
    [SerializeField] TextMeshPro textPrise;
    public GameObject industry;

    [SerializeField] UnityEvent OnBuyProgress;

    private MoneyAnimation _moneyAnimation;
    private string _playerPrefsKey;
    private bool _isWorking = false;
    private bool _isTrigger = false;

    private enum type { Spawner, Manufacture, SellShalf }

    private void Start()
    {
        StartValues();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerController>() != null && !_isWorking && Money.money >= prise)
        {
            StartCoroutine(BuyIndustry());
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
        industry.SetActive(false);

        textPrise.text = prise.ToString();


        _moneyAnimation = GameObject.FindGameObjectWithTag("MoneyAnimation").GetComponent<MoneyAnimation>();
    }

    private void EnableIndustry()
    {
        industry.SetActive(true);
        PlayerPrefs.SetInt("NumberProgress", PlayerPrefs.GetInt("NumberProgress") + 1);
        OnBuyProgress.Invoke();
    }

    private IEnumerator BuyIndustry()
    {
        Debug.Log("StartProgress");
        _isWorking = true;
        yield return new WaitForSeconds(timeToBuy);

        if (_isTrigger)
        {
            StartCoroutine(_moneyAnimation.MoneyPlus(-prise));

            transform.GetChild(0).gameObject.SetActive(false);
            transform.GetChild(1).gameObject.SetActive(false);
            GetComponent<Collider>().enabled = false;

            EnableIndustry();
            yield return new WaitForSeconds(5);
            Destroy(gameObject);
        }
        _isWorking = false;
    }
}
