using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AllCardsUI : MonoBehaviour
{
    [SerializeField] private Transform allCardsContent;
    [SerializeField] private TMP_Dropdown sortDropdown;
    [SerializeField] private Toggle sortToggle;

    private List<CardImage> CardImageList = new List<CardImage>();

    public void LoadAllCards()
    {
        foreach (var cardSO in CardDatabase.Instance.AllCardsSOList)
        {
            CardImage createdCard = Instantiate(Prefabs.GetCardImagePrefab(), allCardsContent).GetComponent<CardImage>();
            createdCard.gameObject.tag = CardDescription.TAG_SHOW_DESCRIPTION;

            createdCard.SetCardSO(cardSO);

            CardImageList.Add(createdCard);
        }

        SortAndDisplayCards();
    }

    // SORTS THE CARDS BASED ON THE SELECTED CRITERIA IN THE DROPDOWN AND TOGGLE
    public void SortAndDisplayCards()
    {
        CardImageSorter.SortCards(CardImageList, (SortType)sortDropdown.value, sortToggle.isOn);

        for (int i = 0; i < CardImageList.Count; i++)
        {
            CardImageList[i].transform.SetSiblingIndex(i);
        }
    }
}
