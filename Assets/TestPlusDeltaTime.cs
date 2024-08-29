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
            Time.timeScale += 0.2f;
            Debug.Log("TimeScaled");
        }

        if (Input.GetKey(KeyCode.M))
        {
            Money.money++;
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            Goyda.Invoke();
        }
    }
}
