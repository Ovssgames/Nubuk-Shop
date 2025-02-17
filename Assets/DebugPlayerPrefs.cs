using UnityEngine;
using YG;
using PlayerPrefs = RedefineYG.PlayerPrefs;

[ExecuteInEditMode]
public class DebugPlayerPrefs : MonoBehaviour
{
    [ContextMenu("Delete All PlayerPrefs")]
    public void ClearPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
    }

    [ContextMenu("SwitchLanguageRu")]
    public void SwitchLanguageRu()
    {
        YG2.SwitchLanguage("ru");
    }

    [ContextMenu("SwitchLanguageTr")]
    public void SwitchLanguageTr()
    {
        YG2.SwitchLanguage("tr");
    }

    [ContextMenu("SwitchLanguageEn")]
    public void SwitchLanguageEn()
    {
        YG2.SwitchLanguage("en");
    }
}
