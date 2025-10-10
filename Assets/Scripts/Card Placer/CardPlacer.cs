using System.Collections.Generic;
using UnityEngine;

public class CardPlacer : MonoBehaviour
{
    [SerializeField] private List<Transform> positionList;

    public List<Card> cardsInPlacer = new List<Card>();

    private Card card = null;

    private void Start()
    {
        Card.OnDragEnded += Card_OnDragEnded;
    }

    private void Card_OnDragEnded(object sender, System.EventArgs e)
    {
        if (card != null)
        {
            if (!AttemptToPlaceCard(card))
            {
                // MAKE CARD IN THE HAND AND PLACE THE CARD TO HAND AGAIN BECAUSE LOCATION IS FULL
            }
        }
    }

    private bool AttemptToPlaceCard(Card card)
    {
        if (cardsInPlacer.Count >= positionList.Count) return false;

        card.transform.position = positionList[cardsInPlacer.Count].transform.position;
        cardsInPlacer.Add(card);
        return true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        card = collision.GetComponent<Card>();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        card = null;

        if (cardsInPlacer.Contains(collision.GetComponent<Card>()))
        {
            cardsInPlacer.Remove(collision.GetComponent<Card>());
        }
    }
}
