using UnityEditor;
using UnityEngine;

public class OpenCloseMenus : MonoBehaviour
{
    [SerializeField] PlayerController playerController;

    public void OpenMenu(GameObject menu)
    {
        menu.SetActive(true);
        playerController.enabled = false;
        Time.timeScale = 0f;
    }
    public void CloseMenu(GameObject menu)
    {
        menu.SetActive(false);
        playerController.enabled = true;
        Time.timeScale = 1f;
    }
}
