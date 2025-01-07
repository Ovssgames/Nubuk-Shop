using UnityEngine;
using System.Diagnostics;
using System.Collections.Generic;
using System.Collections;

public class SaveData : MonoBehaviour
{
    [SerializeField] List<SellShalf> shalfs;
    [SerializeField] float timeToSave;

    [Header("Text")]
    [SerializeField] GameObject saveText;
    [SerializeField] float timeSaveText;


    private Stopwatch _stopwatch;

    private void Start()
    {
        StartValues();
    }


    private void Update()
    {
        if (_stopwatch.Elapsed.TotalSeconds >= timeToSave)
        {
            SaveValues(Money.money);
            ShowText();
        }
    }

    public void SaveValues(int money)
    {
        _stopwatch.Restart();
        Values(money);
        UnityEngine.Debug.Log("Save");
    }

    private void ShowText()
    {
        StartCoroutine(ShowTextSave());
    }

    private IEnumerator ShowTextSave()
    {
        saveText.SetActive(true);
        yield return new WaitForSeconds(timeSaveText);

        saveText.SetActive(false);
        yield break;
    }
    private void Values(int money)
    {
        foreach (SellShalf shalf in shalfs)
        {
            PlayerPrefs.SetInt("ShalfCount" + shalf.type.id, shalf.count);
        }

        PlayerPrefs.SetInt("Money", money);
    }

    private void StartValues()
    {
        _stopwatch = Stopwatch.StartNew();
        saveText.SetActive(false);

        Money.money = PlayerPrefs.GetInt("Money");
    }
}
