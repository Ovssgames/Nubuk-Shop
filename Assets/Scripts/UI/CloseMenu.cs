using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class CloseMenu : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] GameObject menu;

    public void OnPointerClick(PointerEventData eventData)
    {
        Destroy(menu);
    }
}
