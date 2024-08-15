using UnityEngine;
using TMPro;

public class TextUpdater : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI moneyText;

    private void Update()
    {
        moneyText.text = Money.money.ToString();
    }
}
