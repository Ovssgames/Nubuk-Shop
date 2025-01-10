using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ProgressGuysManager : MonoBehaviour
{
    [Header("Player")]
    [Space]
    [SerializeField] Inventory inventory;
    [SerializeField] PlayerController player;
    [SerializeField] List<LevelsProgressGuys> playerLevelsCount;
    [SerializeField] List<LevelsProgressGuys> playerLevelsSpeed;

    [Header("Helpers")]
    [Space]
    [SerializeField] List<NavMeshAgent> helpers;
    [SerializeField] List<LevelsProgressGuys> helpersLevelsSpeed;

    private int _playerLevelCount = 0;
    private int _playerLevelSpeed = 0;
    

    private void Start()
    {
        StartValues();
    }

    public void ProgressCountPlayer()
    {
        if (playerLevelsCount[_playerLevelCount] == null)
            return;

        inventory.count += playerLevelsCount[_playerLevelCount].value;
        PlayerPrefs.SetInt("ProgressCount", inventory.count);
        PlayerPrefs.SetInt("PlayerCount", _playerLevelCount++);
    }

    public void ProgressSpeedPlayer()
    {
        if (playerLevelsSpeed[_playerLevelSpeed] == null)
            return;

        float saveValue = 0;
        player.ChangeSpeed(playerLevelsSpeed[_playerLevelSpeed].value, saveValue);
        PlayerPrefs.SetFloat("ProgressSpeed", saveValue);
        PlayerPrefs.SetInt("PlayerSpeed", _playerLevelSpeed++);
    }

    private void StartValues()
    {
        if (HasKeyCheck("ProgressCount"))
        {
            inventory.count = PlayerPrefs.GetInt("ProgressCount");
        }

        _playerLevelCount = PlayerPrefs.GetInt("PlayerCount");
        _playerLevelSpeed = PlayerPrefs.GetInt("PlayerSpeed");
    }

    private bool HasKeyCheck(string key)
    {
        if(PlayerPrefs.HasKey(key))
            return true;
        else 
            return false;
    }
}

[System.Serializable]
public class LevelsProgressGuys
{
    public int level;
    public int prise;
    public int value;
}
