using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class DialogueSystem : MonoBehaviour
{
    [SerializeField] List<Dialogues> dialogue;
    [SerializeField] TextMeshProUGUI textScene;
    [SerializeField] float speedtext;

    [SerializeField] DialogueAnimation dialogueAnimation;

    private int _index = 0;
    private Coroutine _typingCoroutine;
    [HideInInspector]
    public bool isDoneQuest = false;

    private void OnEnable()
    {
        textScene.text = string.Empty;

        if (!PlayerPrefs.HasKey("PlotIndex"))
            PlayerPrefs.SetInt("PlotIndex", 0);
        if (PlayerPrefs.HasKey("QuestComplite"))
            isDoneQuest = true;

        StartDialogue();
    }

    private void StartDialogue()
    {
        _index = 0;
        StartTyping();
    }

    public void SkipTextClick()
    {
        var plotIndex = PlayerPrefs.GetInt("PlotIndex");

        if (!isDoneQuest)
        {
            if (textScene.text == dialogue[plotIndex].mainDialogue.textDialogue[_index])
            {
                NextText();
            }
            else
            {
                StopTyping();
                textScene.text = dialogue[plotIndex].mainDialogue.textDialogue[_index];
            }
        }
        else
        {
            if (textScene.text == dialogue[plotIndex - 1].waitDialogue.textDialogue[_index])
            {
                NextText();
            }
            else
            {
                StopTyping();
                textScene.text = dialogue[plotIndex - 1].waitDialogue.textDialogue[_index];
            }
        }
    }

    private void NextText()
    {
        var plotIndex = PlayerPrefs.GetInt("PlotIndex");

        if (!isDoneQuest)
        {
            if (_index < dialogue[plotIndex].mainDialogue.textDialogue.Count - 1)
            {
                _index++;
                textScene.text = string.Empty;
                StartTyping();
            }
            else
            {
                dialogue[plotIndex].OnFinishDialogue.Invoke();
                isDoneQuest = true;
                PlayerPrefs.SetInt("PlotIndex", plotIndex + 1);
                PlayerPrefs.SetString("QuestComplite", "true");
                dialogueAnimation.FinishDialogue();
                textScene.text = string.Empty;
                _index = 0;
            }
        }
        else
        {
            if (_index < dialogue[plotIndex - 1].waitDialogue.textDialogue.Count - 1)
            {
                _index++;
                textScene.text = string.Empty;
                StartTyping();
            }
            else
            {
                dialogueAnimation.FinishDialogue();
                textScene.text = string.Empty;
                _index = 0;
            }
        }
    }

    private void StartTyping()
    {
        _typingCoroutine = StartCoroutine(TypeLine());
    }

    private void StopTyping()
    {
        if (_typingCoroutine != null)
        {
            StopCoroutine(_typingCoroutine);
            _typingCoroutine = null;
        }
    }

    private IEnumerator TypeLine()
    {
        var plotIndex = PlayerPrefs.GetInt("PlotIndex");
        if (!isDoneQuest)
        {
            foreach (char c in dialogue[plotIndex].mainDialogue.textDialogue[_index].ToCharArray())
            {
                textScene.text += c;
                yield return new WaitForSeconds(speedtext);
            }
        }
        else
        {
            foreach (char c in dialogue[plotIndex - 1].waitDialogue.textDialogue[_index].ToCharArray())
            {
                textScene.text += c;
                yield return new WaitForSeconds(speedtext);
            }
        }

    }

    [System.Serializable]
    public class Dialogues
    {
        public DialogueText mainDialogue;
        public DialogueText waitDialogue;
        public UnityEvent OnFinishDialogue;
    }
}
