using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlusDeltaTime : MonoBehaviour
{
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
    }
}
