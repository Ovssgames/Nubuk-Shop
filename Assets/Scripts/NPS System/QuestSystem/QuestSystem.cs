using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class QuestSystem : MonoBehaviour
{
    [SerializeField] DialogueSystem dialogueSystem;
    public UnityEvent OnQuestComplite;

    public void QuestComplite()
    {
        PlayerPrefs.DeleteKey("QuestComplite");
        dialogueSystem.isDoneQuest = false;
    }
}
