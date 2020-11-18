using UnityEngine;
using UnityEngine.EventSystems;

public class ClickDetector : MonoBehaviour, IPointerDownHandler, IPointerClickHandler,
    IPointerUpHandler, IPointerExitHandler, IPointerEnterHandler,
    IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public bool isMoving = true;
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (isMoving == false)
        {
            return;
        }
        Debug.Log("Drag Begin");
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (isMoving == false)
        {
            return;
        }
        Debug.Log("Dragging");
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (isMoving == false)
        {
            return;
        }
        Debug.Log("Drag Ended");
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Clicked: " + eventData.pointerCurrentRaycast.gameObject.name);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("Mouse Down: " + eventData.pointerCurrentRaycast.gameObject.name);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("Mouse Enter");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("Mouse Exit");
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Debug.Log("Mouse Up");
    }
}