using System.Collections.Generic;
using UnityEngine;

public class PlayedCardsStackManager : MonoBehaviour
{
    private const int DEFAULT_LAYER_INDEX = 0;
    private int CLICKABLE_LAYER_INDEX = LayerMask.NameToLayer("Clickable");

    public static PlayedCardsStackManager Instance { get; private set; }

    private Stack<Card> cardStack = new Stack<Card>();


    private void Awake()
    {
        Instance = this;
    }

    private void PushCardToStack(Card card)
    {
        MakeTopOfStackCardUnclickable();

        cardStack.Push(card);
    }

    private void PopCardFromStack()
    {
        cardStack.Pop();

        MakeTopOfStackCardClickable();
    }

    private void MakeTopOfStackCardClickable()
    {
        Card card = cardStack.Peek();
        card.gameObject.layer = CLICKABLE_LAYER_INDEX;
    }

    private void MakeTopOfStackCardUnclickable()
    {
        Card card = cardStack.Peek();
        card.gameObject.layer = DEFAULT_LAYER_INDEX;
    }
}
