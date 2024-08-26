using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressAnimation : MonoBehaviour
{
    [SerializeField] float speedAnimation;

    [Range(1f, 2f)]
    [SerializeField] float multiplierScaleAnimation;

    [SerializeField] float errorRateLerp = 0.1f;


    private PlayerController _playerController;

    private void Start()
    {
        _playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    public IEnumerator IndustryEnableAnimation(Transform mashine)
    {
        _playerController.enabled = false;
        Vector3 startScale = mashine.localScale;
        mashine.localScale = Vector3.zero;
        yield return null;

        Vector3 midleScale = new Vector3(startScale.x * multiplierScaleAnimation, startScale.y * multiplierScaleAnimation, startScale.z * multiplierScaleAnimation);
        while (mashine.localScale.x < midleScale.x - errorRateLerp)
        {
            mashine.localScale = Vector3.Lerp(mashine.localScale, midleScale, speedAnimation * Time.deltaTime);
            yield return null;
        }

        mashine.localScale = midleScale;
        yield return null;

        while (mashine.localScale.x > startScale.x + errorRateLerp)
        {
            mashine.localScale = Vector3.Lerp(mashine.localScale, startScale, speedAnimation * Time.deltaTime);
            yield return null;
        }

        mashine.localScale = startScale;
        yield break;
    }
}
