using UnityEngine;
using PlayerPrefs = RedefineYG.PlayerPrefs;

public class QuestSystem : MonoBehaviour
{
    [SerializeField] DialogueSystem dialogueSystem;

    public void QuestComplite()
    {
        PlayerPrefs.DeleteKey("QuestComplite");
        dialogueSystem.isDoneQuest = false;
        PlayerPrefs.Save();
        Debug.Log("QuestCompliete");
    }
}
