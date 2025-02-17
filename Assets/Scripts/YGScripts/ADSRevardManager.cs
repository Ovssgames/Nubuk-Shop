using UnityEngine;
using YG;

public class ADSRevardManager : MonoBehaviour
{
    [SerializeField] MoneyAnimation MoneyAnimation;
    [SerializeField] SaveData _saveData;

    private RevardSize _revardSize;

    private void Start()
    {
        _revardSize = GetComponent<RevardSize>();
    }

    public void OpenADSRevard()
    {
        string id = "coin";
        YG2.RewardedAdvShow(id, Revard);
    }
    
    private void Revard()
    {
        MoneyAnimation.MoneyChange(_revardSize.Revard);
        _saveData.SaveValues(Money.money);
    }
}
