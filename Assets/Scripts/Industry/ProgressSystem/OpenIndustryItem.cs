using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class OpenIndustryItem : MonoBehaviour
{
    [SerializeField] int prise;
    [SerializeField] float timeToBuy;
    [SerializeField] TextMeshPro textPrise;
    public GameObject industry;

    [SerializeField] UnityEvent OnBuyProgress;

    private MoneyAnimation _moneyAnimation;
    private ListProgressObject _listProgress;
    private ProgressAnimation _progressAnimation;
    private bool _isWorking = false;
    private bool _isTrigger = false;


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

        _listProgress = GameObject.FindGameObjectWithTag("GameManager").GetComponent<ListProgressObject>();
        _moneyAnimation = GameObject.FindGameObjectWithTag("MoneyAnimation").GetComponent<MoneyAnimation>();
        _progressAnimation = _listProgress.GetComponent<ProgressAnimation>();
    }

    private void EnableIndustry()
    {
        industry.SetActive(true);
        StartCoroutine(_progressAnimation.IndustryEnableAnimation(industry.transform));
        if (PlayerPrefs.HasKey("NumberProgress") == true)
        {
            PlayerPrefs.SetInt("NumberProgress", PlayerPrefs.GetInt("NumberProgress") + 1);
        }
        else
        {
            PlayerPrefs.SetInt("NumberProgress", 1);
        }
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
            GetComponent<Collider>().enabled = false;

            EnableIndustry();
            yield return new WaitForSeconds(4);

            var number = PlayerPrefs.GetInt("NumberProgress");

            if(_listProgress.OpenItems.Count > number)
                _listProgress.NextProgress(number);
            Destroy(gameObject);
        }
        _isWorking = false;
    }
}
