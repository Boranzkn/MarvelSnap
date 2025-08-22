using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class Card : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public static event EventHandler OnDragEnded;

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("");
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = (Vector2) Camera.main.ScreenToWorldPoint(eventData.position);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("");

        OnDragEnded?.Invoke(this, EventArgs.Empty);
    }
}
