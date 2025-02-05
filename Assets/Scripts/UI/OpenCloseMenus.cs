using UnityEditor;
using UnityEngine;

public class OpenCloseMenus : MonoBehaviour
{
    [SerializeField] PlayerController playerController;

    public void OpenMenu(GameObject menu)
    {
        menu.SetActive(true);
        playerController.enabled = false;
    }
    public void CloseMenu(GameObject menu)
    {
        menu.SetActive(false);
        playerController.enabled = true;
    }
}
