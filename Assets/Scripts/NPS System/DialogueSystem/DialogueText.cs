using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[CreateAssetMenu(fileName = "New Dialogue", menuName = "ScriptableObject/Dialogue")]
public class DialogueText : ScriptableObject
{
    [TextArea(2, 5)]
    public List<string> textDialogue;
}
