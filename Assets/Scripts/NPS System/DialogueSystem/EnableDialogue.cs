using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EnableDialogue : MonoBehaviour
{
    private DialogueAnimation _dialogueAnimation;

    private void Start()
    {
        StartValues();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
            StartDialogue();
    }

    private void StartValues()
    {
        _dialogueAnimation = GameObject.FindGameObjectWithTag("GameManager").GetComponent<DialogueAnimation>();
    }

    private void StartDialogue()
    {
        _dialogueAnimation.OnDialogueHappen.Invoke();
    }
}
