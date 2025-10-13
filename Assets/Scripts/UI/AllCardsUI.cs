using UnityEngine;
using UnityEngine.UI;

public class AllCardsUI : MonoBehaviour
{
    [SerializeField] private Transform allCardsContent;

    public void LoadAllCards()
    {
        foreach (var cardSO in CardDatabase.Instance.AllCardsSOList)
        {
            CardImage createdCard = Instantiate(Prefabs.GetCardImagePrefab(), allCardsContent).GetComponent<CardImage>();
            createdCard.gameObject.tag = CardDescription.TAG_SHOW_DESCRIPTION;

            createdCard.SetCardSO(cardSO);
        }
    }
}
