using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class Card : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public static event EventHandler OnDragEnded;

    [HideInInspector] public CardPositionState CardPositionState = CardPositionState.Deck;

    [SerializeField] private CardSO cardSO;

    public void OnBeginDrag(PointerEventData eventData)
    {

    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = (Vector2) Camera.main.ScreenToWorldPoint(eventData.position);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        OnDragEnded?.Invoke(this, EventArgs.Empty);
    }

    public void PerformAbility()
    {

    }
}