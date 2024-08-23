using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListProgressObject : MonoBehaviour
{
    public List<OpenIndustryItem> OpenItems;



    private void Awake()
    {
        AwakeValues();   
    }

    private void AwakeValues()
    {
        var numser = PlayerPrefs.GetInt("NumberProgress");
        for (int i = 0; i < numser; i++)
        {
            OpenItems[i].industry.SetActive(true);
        }
    }
}
