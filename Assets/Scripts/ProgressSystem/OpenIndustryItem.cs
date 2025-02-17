using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using TMPro;
using PlayerPrefs = RedefineYG.PlayerPrefs;

public class OpenIndustryItem : MonoBehaviour
{
    public int prise;
    [SerializeField] float timeToBuy;
    [SerializeField] TextMeshPro textPrise;
    [SerializeField] ParticleSystem particle;
    public GameObject industry;

    [SerializeField] UnityEvent OnBuyProgress;
    [SerializeField] SaveData saveData;
    [SerializeField] AudioSource audioSource;

    private MoneyAnimation _moneyAnimation;
    private ListProgressObject _listProgress;
    private ProgressAnimation _progressAnimation;
    private NextCameraProgress _nextCameraProgress;
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
            particle.Play();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<PlayerController>() != null)
        {
            _isTrigger = false;
            particle.Stop();
        }
    }

    private void StartValues()
    {
        industry.SetActive(false);

        textPrise.text = prise.ToString();

        _listProgress = GameObject.FindGameObjectWithTag("GameManager").GetComponent<ListProgressObject>();
        _moneyAnimation = GameObject.FindGameObjectWithTag("MoneyAnimation").GetComponent<MoneyAnimation>();
        _progressAnimation = _listProgress.GetComponent<ProgressAnimation>();
        _nextCameraProgress = _listProgress.GetComponent<NextCameraProgress>();

        particle.Stop();
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
        PlayerPrefs.Save();
    }

    private IEnumerator BuyIndustry()
    {
        _isWorking = true;
        yield return new WaitForSeconds(timeToBuy);

        if (_isTrigger)
        {
            saveData.SaveValues(Money.money - prise);
            _moneyAnimation.MoneyChange(-prise);
            audioSource.Play();
            OnBuyProgress.Invoke();

            transform.GetChild(0).gameObject.SetActive(false);
            GetComponent<Collider>().enabled = false;

            EnableIndustry();

            yield return null;
            var number = PlayerPrefs.GetInt("NumberProgress");

            if (_listProgress.OpenItems.Count > number)
            {
                _listProgress.NextProgress(number);
            }
            yield return new WaitForSeconds(1f);
            OnBuyProgress.Invoke();

            if (_listProgress.OpenItems.Count > number)
            {
                _nextCameraProgress.NextCamera(_listProgress.OpenItems[number].gameObject);
            }
            yield return null;

            Destroy(gameObject);
        }
        _isWorking = false;
    }
}
