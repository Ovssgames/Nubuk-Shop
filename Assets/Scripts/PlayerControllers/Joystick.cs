using UnityEngine;
using UnityEngine.EventSystems;

public class Joystick : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    public RectTransform background; // фон джойстика
    public RectTransform handle; // ручка джойстика
    private Canvas canvas;
    private RectTransform canvasRectTransform;
    private Vector2 inputVector;

    private void Start()
    {
        canvas = GetComponentInParent<Canvas>();
        canvasRectTransform = canvas.GetComponent<RectTransform>();
        background.gameObject.SetActive(false);

        if (PlayerController.platform != "Mobile")
            Destroy(gameObject);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        background.gameObject.SetActive(true);
        background.position = eventData.position;
        OnDrag(eventData);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        inputVector = Vector2.zero;
        background.gameObject.SetActive(false);
        handle.anchoredPosition = Vector2.zero;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 pos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRectTransform, eventData.position, eventData.pressEventCamera, out pos);
        Vector2 joystickPos = pos - (Vector2)background.anchoredPosition;

        if (joystickPos.magnitude > background.sizeDelta.x / 2)
        {
            joystickPos = joystickPos.normalized * background.sizeDelta.x / 2;
        }

        handle.anchoredPosition = joystickPos;
        inputVector = joystickPos / (background.sizeDelta.x / 2);
    }

    public Vector2 GetDirection()
    {
        return inputVector;
    }
}

