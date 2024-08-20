using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class OpenIndustryItem : MonoBehaviour
{
    [SerializeField] type typeIndustry;
    [SerializeField] int prise;
    [SerializeField] float timeToBuy;
    [SerializeField] TextMeshPro textPrise;
    [SerializeField] GameObject industry;
    [SerializeField] List<GameObject> nextOpenIndustry;


    private MoneyAnimation _moneyAnimation;
    private string _playerPrefsKey;
    private bool _isWorking = false;
    private bool _isTrigger = false;

    private enum type { Spawner, Manufacture, SellShalf }

    private void Awake()
    {
        AwakeValues();
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

    private void AwakeValues()
    {
        industry.SetActive(false);

        if (typeIndustry == type.Manufacture)
            _playerPrefsKey = "Manufacture" + industry.GetComponent<Manufacture>().finishType.id;
        else if (typeIndustry == type.Spawner)
            _playerPrefsKey = "Spawner" + industry.GetComponent<Spawner>().propertisObject.id;
        else
            _playerPrefsKey = "SellSfalf" + industry.GetComponent<SellShalf>().type.id;

        _moneyAnimation = GameObject.FindGameObjectWithTag("MoneyAnimation").GetComponent<MoneyAnimation>();

        if (PlayerPrefs.HasKey(_playerPrefsKey))
        {
            EnableIndustry();
        }
    }

    private void EnableIndustry()
    {
        industry.SetActive(true);
        Destroy(gameObject);
    }

    private IEnumerator BuyIndustry()
    {
        Debug.Log("StartProgress");
        _isWorking = true;
        yield return new WaitForSeconds(timeToBuy);

        if (_isTrigger)
        {
            EnableIndustry();
            _moneyAnimation.MoneyPlus(-prise);
        }
    }
}
