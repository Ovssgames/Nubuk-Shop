using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class DialogueAnimation : MonoBehaviour
{
    [Header("Animation Objects")]
    [SerializeField] List<DialogueAnimationPoints> points;
    [SerializeField] Image background;

    [Header("Animations Values")]
    [SerializeField] float speed;
    [Range(0f, 1f)]
    [SerializeField] float backgroundAlpha;

    [SerializeField] DialogueSystem dialogueSystem;
    [SerializeField] GameObject buttonSkip;

    public UnityEvent OnDialogueHappen;

    [HideInInspector]
    public bool isOpen = false;
    private PlayerController _playerController;


    private void Start()
    {
        StartValues();
    }

    private void StartValues()
    {

        foreach (var item in points)
        {
            item.animationObject.position = item.startPosition.position;
        }

        dialogueSystem.enabled = false;
        dialogueSystem.gameObject.SetActive(false);

        _playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    public void StartDialogue()
    {
        StartCoroutine(AnimationOpen());
    }

    public void FinishDialogue()
    {
        StartCoroutine(AnimationClose());
    }

    private IEnumerator AnimationOpen()
    {
        EnableElements(false);
        yield return new WaitForSeconds(0.4f);

        Color colorBackground = background.color;

        while (colorBackground.a < backgroundAlpha - 0.01f)
        {
            colorBackground.a = Mathf.Lerp(colorBackground.a, backgroundAlpha, speed * Time.deltaTime);
            background.color = colorBackground;
            yield return null;
        }
        yield return new WaitForSeconds(0.2f);

        foreach (var item in points)
        {
            float distanse = Vector3.Distance(item.animationObject.position, item.finishPosition.position);
            while (distanse > 0.5f)
            {
                distanse = Vector3.Distance(item.animationObject.position, item.finishPosition.position);
                item.animationObject.position = Vector3.Lerp(item.animationObject.position, item.finishPosition.position, Time.deltaTime * speed);
                yield return null;
            }

            item.animationObject.position = item.finishPosition.position;
            yield return new WaitForSeconds(0.25f);
        }

        dialogueSystem.enabled = true;
        buttonSkip.SetActive(true);
    }

    private IEnumerator AnimationClose()
    {
        dialogueSystem.enabled = false;
        buttonSkip.SetActive(false);

        for(int i = points.Count - 1; i > -1; i--)
        {
            float distanse = Vector3.Distance(points[i].animationObject.position, points[i].startPosition.position);
            while (distanse > 0.5f)
            {
                distanse = Vector3.Distance(points[i].animationObject.position, points[i].startPosition.position);
                points[i].animationObject.position = Vector3.Lerp(points[i].animationObject.position, points[i].startPosition.position, Time.deltaTime * speed);
                yield return null;
            }

            points[i].animationObject.position = points[i].startPosition.position;
            yield return null;
        }
        yield return null;

        Color colorBackground = background.color;
        
        while (colorBackground.a > 0f + 0.01f)
        {
            colorBackground.a = Mathf.Lerp(colorBackground.a, 0, speed * Time.deltaTime);
            background.color = colorBackground;
            yield return null;
        }

        EnableElements(true);
    }

    private void EnableElements(bool meaning)
    {
        _playerController.enabled = meaning;

        dialogueSystem.gameObject.SetActive(meaning == false);
        isOpen = meaning == false;
    }
}


[System.Serializable]
public class DialogueAnimationPoints
{
    public Transform animationObject;

    [Header("Points")]
    public Transform startPosition;
    public Transform finishPosition;
}