using UnityEngine;
using UnityEngine.EventSystems;

public class Joystick : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    public float deadZone = 0.2f; // радиус мёртвой зоны, в диапазоне от 0 до 1
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
            gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        inputVector = Vector2.zero;
        background.gameObject.SetActive(false);
        handle.anchoredPosition = Vector2.zero;
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

        // Применение мёртвой зоны
        if (joystickPos.magnitude < deadZone * (background.sizeDelta.x / 2))
        {
            inputVector = Vector2.zero;
            handle.anchoredPosition = Vector2.zero;
            return;
        }

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

