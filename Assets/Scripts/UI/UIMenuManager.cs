using OpenCover.Framework.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMenuManager : MonoBehaviour
{
    [SerializeField] List<Section> section;

    [Space]
    [Header("Main menu animation")]
    [SerializeField] List<MonoBehaviour> disableScripts;
    [SerializeField] CanvasGroup mainMenu;
    [SerializeField] CloseShop closeShop;
    [SerializeField] float speedAnimation;

    [Space]
    [Header("Color buttons")]
    [SerializeField] Color activeMenu;
    [SerializeField] Color inactiveMenu;

    private void Start()
    {
        StartValues();
    }

    public void SetActiveMainMenu(bool active)
    {
        StartCoroutine(MainMenuAnimation(active));
    }

    public void DirectSection(int id)
    {
        foreach (Section item in section)
        {
            item.menuButton.color = inactiveMenu;
            item.menu.SetActive(false);
        }

        section[id].menu.SetActive(true);
        section[id].menuButton.color = activeMenu;
    }

    private IEnumerator MainMenuAnimation(bool active)
    {
        if (active)
        {
            mainMenu.gameObject.SetActive(active);
            mainMenu.interactable = false;
            DirectSection(0);
            foreach (MonoBehaviour item in disableScripts) item.enabled = false;

            yield return null;
            while (mainMenu.alpha < 0.99f)
            {
                mainMenu.alpha = Mathf.Lerp(mainMenu.alpha, 1, Time.deltaTime * speedAnimation);
                yield return null;
            }

            mainMenu.alpha = 1f;
            mainMenu.interactable = true;
            closeShop.enabled = true;

            yield break;
        }
        else
        {
            mainMenu.interactable = false;
            closeShop.enabled = false;

            yield return null;
            while (mainMenu.alpha > 0.01f)
            {
                mainMenu.alpha = Mathf.Lerp(mainMenu.alpha, 0, Time.deltaTime * speedAnimation);
                yield return null;
            }

            foreach (MonoBehaviour item in disableScripts) item.enabled = true;
            mainMenu.alpha = 0f;
            mainMenu.gameObject.SetActive(active);

            yield break;
        }
    }

    private void StartValues()
    {
        mainMenu.gameObject.SetActive(false);
        mainMenu.alpha = 0f;
        closeShop.enabled = false;
        foreach (Section item in section) item.menu.SetActive(false);
    }
}

[System.Serializable]
public class Section
{
    public Image menuButton;
    public GameObject menu;
}