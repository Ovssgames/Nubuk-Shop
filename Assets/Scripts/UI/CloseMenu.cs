using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class CloseMenu : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] GameObject menu;

    public void OnPointerDown(PointerEventData eventData)
    {
        Destroy(menu);
    }
}
