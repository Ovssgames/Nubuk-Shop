using System.Collections;
using UnityEngine;

public class PalezAnimation : MonoBehaviour
{
    [SerializeField] float finishSize;
    [SerializeField] float speed;
    [SerializeField] float waitTime;

    private float startSize;

    private void Start()
    {
        StartValues();
    }

    private void StartValues()
    {
        startSize = transform.localScale.x;
        StartCoroutine(Animation());
    }

    private IEnumerator Animation()
    {
        while (!IsFinishPosition(finishSize))
        {
            transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(finishSize, finishSize, startSize), Time.deltaTime * speed);
            yield return null;
        }
        yield return new WaitForSeconds(waitTime);

        while (!IsFinishPosition(startSize))
        {
            transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(startSize, startSize, startSize), Time.deltaTime * speed);
            yield return null;
        }
        yield return new WaitForSeconds(waitTime);

        StartCoroutine(Animation());
        yield break;
    }

    private bool IsFinishPosition(float finish)
    {
        if (Vector3.Distance(transform.localScale, new Vector3(finish, finish, startSize)) < 0.01f)
            return true;
        else
            return false;
    }
}
