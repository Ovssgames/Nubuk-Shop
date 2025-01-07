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
        var number = PlayerPrefs.GetInt("NumberProgress");

        if (number != 0)
        {
            for (int i = 0; i < number; i++)
            {
                OpenItems[i].industry.SetActive(true);
                Destroy(OpenItems[i].gameObject);
            }
            if(OpenItems.Count > number)
                OpenItems[number].gameObject.SetActive(true);

        }
        else
        {
            OpenItems[number].gameObject.SetActive(true);
        }
    }

    public void NextProgress(int number)
    {
        GameObject progress = OpenItems[number].gameObject;
        if (progress != null)
            progress.SetActive(true);
    }
}
