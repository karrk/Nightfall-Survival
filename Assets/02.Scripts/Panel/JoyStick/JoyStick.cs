using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.OnScreen;
using static UnityEngine.Rendering.VirtualTexturing.Debugging;

public class JoyStick : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    [SerializeField]
    private RectTransform bg;
    
    [SerializeField]
    private RectTransform handle;
    
    [InputControl(layout = "Vector2")]
    [SerializeField]
    private string stickControlPath;

    [SerializeField]
    private float movementRange = 100.0f;

    private Canvas canvas;
    protected CanvasGroup bgCanvasGroup;
    protected RectTransform baseRect;

    protected OnScreenStick handleStickController;

    protected Vector2 initialPosition = Vector2.zero;


    protected virtual void Awake()
    {
        canvas = GetComponentInParent<Canvas>();
        baseRect = GetComponent<RectTransform>();
        bgCanvasGroup = GetComponent<CanvasGroup>();

        handleStickController = handle.gameObject.AddComponent<OnScreenStick>();
        handleStickController.movementRange = movementRange;
        handleStickController.controlPath = stickControlPath;

        Vector2 center = new Vector2(0.5f, 0.5f);
        bg.pivot = center;

        handle.anchorMin = center;
        handle.anchorMax = center;
        handle.pivot = center;
        handle.anchoredPosition = Vector2.zero;

        initialPosition = bg.anchoredPosition;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        PointerEventData constructedEventData = new PointerEventData(EventSystem.current);
        constructedEventData.position = handle.position;
        handleStickController.OnPointerDown(constructedEventData);

        bg.anchoredPosition = GetAnchoredPosition(eventData.position);
    }

    public void OnDrag(PointerEventData eventData)
    {
        handleStickController.OnDrag(eventData);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        initialPosition = bg.anchoredPosition;

        PointerEventData constructedEventData = new PointerEventData(EventSystem.current);
        constructedEventData.position = Vector2.zero;

        handleStickController.OnPointerUp(constructedEventData);

    }

    public Vector2 GetAnchoredPosition(Vector2 screenPosition)
    {
        Camera cam = (canvas.renderMode == RenderMode.ScreenSpaceCamera) ? canvas.worldCamera : null;

        Vector2 localPoint = Vector2.zero;

        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(baseRect, screenPosition, cam, out localPoint))
        {
            Vector2 pivotOffset = baseRect.pivot * baseRect.sizeDelta;
            return localPoint - (bg.anchorMax * baseRect.sizeDelta) + pivotOffset;
        }

        return Vector2.zero;
    }

    public Vector2 GetDirection()
    {
        if(initialPosition != bg.anchoredPosition)
        {
            return Vector2.zero;
        }

        return handle.anchoredPosition;
    }
}
