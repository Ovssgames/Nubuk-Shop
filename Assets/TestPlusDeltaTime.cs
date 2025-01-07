using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TestPlusDeltaTime : MonoBehaviour
{
    public UnityEvent Goyda;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            Time.timeScale += 0.5f;
        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            FindObjectOfType<MoneyAnimation>().MoneyChange(10);
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            Goyda.Invoke();
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            FindObjectOfType<SaveData>().SaveValues(Money.money);
        }
    }
}
