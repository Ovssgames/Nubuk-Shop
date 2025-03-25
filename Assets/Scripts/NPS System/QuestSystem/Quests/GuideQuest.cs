using System.Collections.Generic;
using UnityEngine;
using PlayerPrefs = RedefineYG.PlayerPrefs;

public class GuideQuest : MonoBehaviour
{
    [Tooltip("Вставлять прогресс объекты")]
    [SerializeField] List<OpenIndustryItem> ProgressLevels;
    [SerializeField] QuestSystem questSystem;
    [SerializeField] GameObject framePNS;

    private void Start()
    {
        StartValues();
    }

    private void StartValues()
    {
        EnableProgressObject();
        if (PlayerPrefs.GetInt("PlotIndex") > ProgressLevels.Count)
            Destroy(this);

        if (!PlayerPrefs.HasKey("QuestComplite"))
            framePNS.SetActive(true);
        else
            framePNS.SetActive(false);
    }

    private void EnableProgressObject()
    {
        var plotIndex = PlayerPrefs.GetInt("PlotIndex");
        if (plotIndex < ProgressLevels.Count)
            ProgressLevels[plotIndex].GetComponent<BoxCollider>().enabled = false;
        else
            Destroy(this);
    }

    public void QuestBranchCompliete()
    {
        questSystem.QuestComplite();
        framePNS.SetActive(true);

        EnableProgressObject();
        PlayerPrefs.Save();
    }

    public void NextQuest()
    {
        var plotIndex = PlayerPrefs.GetInt("PlotIndex");
        framePNS.SetActive(false);

        ProgressLevels[plotIndex].GetComponent<BoxCollider>().enabled = true;
        PlayerPrefs.Save();
    }
}
