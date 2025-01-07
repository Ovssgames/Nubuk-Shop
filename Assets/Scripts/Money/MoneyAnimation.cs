using System.Collections;
using UnityEngine;
using TMPro;

public class MoneyAnimation : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textMoney;
    [SerializeField] GameObject textParticle;
    [SerializeField] Transform spawnPosirion;
    [SerializeField] Transform finishPosition;

    [Header("Text Settings")]
    [SerializeField] float speed;
    [SerializeField] float timeWait = 0.5f;

    [Header("   Color")]
    [SerializeField] Color colorPositive;
    [SerializeField] Color colorNegative;

    [Header("Money Settings")]
    [SerializeField] float speedMoney = 10;

    private void Start()
    {
        StartValues();
    }

    private void StartValues()
    {
        textMoney.text = Money.money.ToString();
    }

    public void MoneyChange(int count)
    {
        StartCoroutine(MoneyPlus(count));
    }

    private IEnumerator MoneyPlus(int count)
    {
        var prefab = Instantiate(textParticle);
        prefab.transform.SetParent(transform, false);
        prefab.transform.position = spawnPosirion.position;
        int moneyFinish = Money.money + count;
        Money.money = moneyFinish;

        TextMeshProUGUI textPrefab = prefab.GetComponent<TextMeshProUGUI>();
        if (count >= 0)
        {
            textPrefab.color = colorPositive;
            textPrefab.text = "+" + count.ToString();
        }
        else
        {
            textPrefab.color = colorNegative;
            textPrefab.text = count.ToString();
        }
        yield return null;

        Color colorText = textPrefab.color;
        while (prefab.transform.position.y < finishPosition.position.y - 5f)
        {
            prefab.transform.position = Vector3.Lerp(prefab.transform.position, finishPosition.position, speed * Time.deltaTime);
            colorText.a = Mathf.Lerp(colorText.a, 0.5f, speed * Time.deltaTime);
            textPrefab.color = colorText;
            yield return null;
        }
        yield return new WaitForSeconds(timeWait);

        Destroy(prefab);

        float text = int.Parse(textMoney.text);
        while (text != moneyFinish)
        {
            text = Mathf.MoveTowards(text, moneyFinish, Time.deltaTime * speedMoney);
            textMoney.text = ((int)text).ToString();
            yield return null;
        }
        yield break;
    }
}
