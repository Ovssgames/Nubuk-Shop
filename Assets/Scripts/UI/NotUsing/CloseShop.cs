using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class CloseShop : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] UnityEvent OnClick;

    public void OnPointerClick(PointerEventData eventData)
    {
        OnClick.Invoke();
    }
}
