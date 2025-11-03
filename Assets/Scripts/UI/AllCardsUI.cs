using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AllCardsUI : MonoBehaviour
{
    [SerializeField] private Transform allCardsContent;
    [SerializeField] private TMP_Dropdown sortDropdown;
    [SerializeField] private TMP_Dropdown abilityDropdown;
    [SerializeField] private TMP_InputField searchInputField;
    [SerializeField] private Toggle sortToggle;
    [SerializeField] private CostToggles costToggles;

    private List<CardImage> cardImageList = new List<CardImage>();

    public void LoadAllCards()
    {
        foreach (var cardSO in CardDatabase.Instance.AllCardsSOList)
        {
            CardImage createdCard = Instantiate(Prefabs.GetCardImagePrefab(), allCardsContent).GetComponent<CardImage>();
            createdCard.gameObject.tag = CardDescription.TAG_SHOW_DESCRIPTION;

            createdCard.SetCardSO(cardSO);

            cardImageList.Add(createdCard);
        }

        SortAndDisplayCards();
    }

    public void ApplyFilters()
    {
        var filteredCards = cardImageList.FindAll(card => (string.IsNullOrEmpty(searchInputField.text) || card.GetCardSO().GetCardName().ToLower().Contains(searchInputField.text.ToLower()))
                                                        && (abilityDropdown.value == -1 || card.GetCardSO().GetAbilityType() == (CardAbilityType)abilityDropdown.value)
                                                        && (costToggles.GetSelectedCostTogglesIndex().Count == 0 || costToggles.GetSelectedCostTogglesIndex().Contains(card.GetCardSO().GetCost())));

        foreach (var card in cardImageList)
        {
            card.gameObject.SetActive(filteredCards.Contains(card));
        }
    }

    public void SortAndDisplayCards()
    {
        CardImageSorter.SortCards(cardImageList, (SortType)sortDropdown.value, sortToggle.isOn);
        ChangePositionOfCards();
    }

    private void ChangePositionOfCards()
    {
        for (int i = 0; i < cardImageList.Count; i++)
        {
            cardImageList[i].transform.SetSiblingIndex(i);
        }
    }

    public void ResetSearch()
    {
        searchInputField.text = string.Empty;

        ApplyFilters();
    }
}
