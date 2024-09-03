using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GuideQuest : MonoBehaviour
{
    [Tooltip("Вставлять прогресс объекты")]
    [SerializeField] List<OpenIndustryItem> ProgressLevels;
    [SerializeField] QuestSystem questSystem;

    private void Start()
    {
        StartValues();
    }

    private void StartValues()
    {
        EnableProgressObject();
        if (PlayerPrefs.GetInt("PlotIndex") > ProgressLevels.Count)
            Destroy(this);
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
        questSystem.OnQuestComplite.Invoke();

        EnableProgressObject();
    }

    public void NextQuest()
    {
        var plotIndex = PlayerPrefs.GetInt("PlotIndex");

        ProgressLevels[plotIndex].GetComponent<BoxCollider>().enabled = true;
    }
}
