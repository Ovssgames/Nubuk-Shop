using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ProgressGuysManager : MonoBehaviour
{
    [Space]
    [Header("Player")]
    [SerializeField] Inventory inventory;
    [SerializeField] PlayerController player;

    [Space]
    [SerializeField] List<LevelsProgressGuys> playerLevelsCount;
    [SerializeField] TextMeshProUGUI levelCountText;
    [SerializeField] TextMeshProUGUI priseCountText;

    [SerializeField] List<LevelsProgressGuys> playerLevelsSpeed;
    [SerializeField] TextMeshProUGUI levelSpeedText;
    [SerializeField] TextMeshProUGUI priseSpeedText;


    private void Start()
    {
        StartValues();
    }  

    public void ProgressLevelCountPlayer()
    {
        var id = PlayerPrefs.GetInt("PlayerProgressCount");

        if(id >= playerLevelsCount.Count)
            return;

        inventory.count = playerLevelsCount[id].value;

        id++;
        PlayerPrefs.SetInt("PlayerProgressCount", id);

        UpdateText(priseCountText, playerLevelsCount[id].prise.ToString());
        UpdateText(levelCountText, (id + "/" + playerLevelsCount.Count).ToString());
    }

    public void ProgressLevelSpeedPlayer()
    {
        var id = PlayerPrefs.GetInt("PlayerProgressSpeed");

        if (id >= playerLevelsSpeed.Count)
            return;

        player.speed = playerLevelsSpeed[id].value;

        id++;
        PlayerPrefs.SetInt("PlayerProgressSpeed", id);

        UpdateText(priseSpeedText, playerLevelsSpeed[id].prise.ToString());
        UpdateText(levelSpeedText, (id + "/" + playerLevelsSpeed.Count).ToString());
    }

    private void StartValues()
    {
        var idCount = PlayerPrefs.GetInt("PlayerProgressCount") - 1;

        UpdateText(priseCountText, playerLevelsCount[idCount].prise.ToString());
        UpdateText(levelCountText, (idCount + "/" + playerLevelsCount.Count).ToString());

        var idSpeed = PlayerPrefs.GetInt("PlayerProgressSpeed") - 1;

        UpdateText(priseSpeedText, playerLevelsSpeed[idSpeed].prise.ToString());
        UpdateText(levelSpeedText, (idSpeed + "/" + playerLevelsSpeed.Count).ToString());
    }

    private void UpdateText(TextMeshProUGUI textGameObject, string text)
    {
        textGameObject.text = text;
    }

    private bool HasKeyCheck(string key)
    {
        return PlayerPrefs.HasKey(key);
    }
}

[System.Serializable]
public class LevelsProgressGuys
{
    public int prise;
    public int value;
}
