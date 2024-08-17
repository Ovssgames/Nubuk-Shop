using System.Collections;
using UnityEngine;
using TMPro;

public class MoneyAnimation : MonoBehaviour
{
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

    public IEnumerator MoneyPlus(int count)
    {
        var prefab = Instantiate(textParticle);
        prefab.transform.SetParent(transform, false);
        prefab.transform.position = spawnPosirion.position;

        TextMeshProUGUI textPrefab = prefab.GetComponent<TextMeshProUGUI>();
        if (count >= 0)
        {
            textPrefab.color = colorPositive;
            textPrefab.text = "+" + count.ToString();
        }
        else
        {
            textPrefab.color = colorNegative;
            textPrefab.text = "-" + count.ToString();
        }
        yield return null;

        while (prefab.transform.position.y < finishPosition.position.y)
        {
            prefab.transform.position += new Vector3(0, Time.deltaTime * speed, 0);
            yield return null;
        }
        yield return new WaitForSeconds(timeWait);

        Destroy(prefab);

        int moneyFinish = Money.money + count;
        while (Money.money < moneyFinish)
        {
            Money.money = (int)Mathf.MoveTowards(Money.money, moneyFinish, Time.deltaTime + speedMoney);
            yield return null;
        }
        yield break;
    }
}
