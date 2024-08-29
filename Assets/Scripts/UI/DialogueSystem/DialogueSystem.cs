using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueSystem : MonoBehaviour
{
    [SerializeField] List<DialogueText> dialogue;
    [SerializeField] TextMeshProUGUI textScene;
    [SerializeField] float speedtext;

    [SerializeField] DialogueAnimation dialogueAnimation;

    private int _index = 0;
    private Coroutine _typingCoroutine;

    private void Start()
    {
        textScene.text = string.Empty;

        if (!PlayerPrefs.HasKey("PlotIndex"))
            PlayerPrefs.SetInt("PlotIndex", 0);

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
        if (textScene.text == dialogue[plotIndex].textDialogue[_index])
        {
            NextText();
        }
        else
        {
            StopTyping();
            textScene.text = dialogue[plotIndex].textDialogue[_index];
        }
    }

    private void NextText()
    {
        var plotIndex = PlayerPrefs.GetInt("PlotIndex");
        if (_index < dialogue[plotIndex].textDialogue.Count - 1)
        {
            _index++;
            textScene.text = string.Empty;
            StartTyping();
        }
        else
        {
            PlayerPrefs.SetInt("PlotIndex", plotIndex + 1);
            dialogueAnimation.FinishDialogue();
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

        foreach (char c in dialogue[plotIndex].textDialogue[_index].ToCharArray())
        {
            textScene.text += c;
            yield return new WaitForSeconds(speedtext);
        }
    }
}
