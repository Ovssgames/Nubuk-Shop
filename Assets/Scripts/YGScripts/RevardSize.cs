using TMPro;
using UnityEngine;

public class RevardSize : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI revardText;
    private int _revard;

    public int Revard
    {
        get 
        { 
            return _revard; 
        }
        set 
        {   
            if (value != 0)
            {
                _revard = value;
                revardText.text = value.ToString();
            }
            else
            {
                _revard = 20;
                revardText.text = "20";
            }
        }
    }
}
