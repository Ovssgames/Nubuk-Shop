using System.Collections.Generic;
using UnityEngine;
using PlayerPrefs = RedefineYG.PlayerPrefs;

public class ListProgressObject : MonoBehaviour
{
    public List<OpenIndustryItem> OpenItems;
    private RevardSize _revardSize;

    private void Awake()
    {
        AwakeValues();   
    }

    private void AwakeValues()
    {
        _revardSize = GameObject.FindGameObjectWithTag("YGManager").GetComponent<RevardSize>();

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

        _revardSize.Revard = Mathf.FloorToInt(OpenItems[number].prise * 1.2f);
    }

    public void NextProgress(int number)
    {
        GameObject progress = OpenItems[number].gameObject;
        if (progress != null)
            progress.SetActive(true);
        _revardSize.Revard = Mathf.FloorToInt(OpenItems[number].prise * 1.2f);
    }
}
