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

        SortByDesiredValues();
    }






    // SORTS THE CARDS BASED ON THE SELECTED CRITERIA IN THE DROPDOWN AND TOGGLE
    public void SortByDesiredValues()
    {
        switch (sortDropdown.value)
        {
            case 0: // Cost
                if (sortToggle.isOn)
                    SortByCostIncreasing();
                else
                    SortByCostDecreasing();
                break;
            case 1: // Power
                    if (sortToggle.isOn)
                    SortByPowerIncreasing();
                else
                    SortByPowerDecreasing();
                break;
            case 2: // Name
                if (sortToggle.isOn)
                    SortByNameIncreasing();
                else
                    SortByNameDecreasing();
                break;
            default:
                break;
        }

        for (int i = 0; i < CardImageList.Count; i++)
        {
            CardImageList[i].transform.SetSiblingIndex(i);
        }
    }

    private void SortByCostIncreasing()
    {
        CardImageList.Sort((left, right) =>
        {
            int costComparison = left.GetCardSO().GetCost().CompareTo(right.GetCardSO().GetCost());
            if (costComparison == 0)
            {
                // If costs are the same, compare by name
                return string.Compare(left.GetCardSO().GetCardName(), right.GetCardSO().GetCardName(), StringComparison.Ordinal);
            }
            return costComparison;
        });
    }

    private void SortByCostDecreasing()
    {
        CardImageList.Sort((left, right) =>
        {
            int costComparison = right.GetCardSO().GetCost().CompareTo(left.GetCardSO().GetCost());
            if (costComparison == 0)
            {
                // If costs are the same, compare by name
                return string.Compare(right.GetCardSO().GetCardName(), left.GetCardSO().GetCardName(), StringComparison.Ordinal);
            }
            return costComparison;
        });
    }

    private void SortByPowerIncreasing()
    {
        CardImageList.Sort((left, right) =>
        {
            int costComparison = left.GetCardSO().GetPower().CompareTo(right.GetCardSO().GetPower());
            if (costComparison == 0)
            {
                // If costs are the same, compare by name
                return string.Compare(left.GetCardSO().GetCardName(), right.GetCardSO().GetCardName(), StringComparison.Ordinal);
            }
            return costComparison;
        });
    }

    private void SortByPowerDecreasing()
    {
        CardImageList.Sort((left, right) =>
        {
            int costComparison = right.GetCardSO().GetPower().CompareTo(left.GetCardSO().GetPower());
            if (costComparison == 0)
            {
                // If costs are the same, compare by name
                return string.Compare(right.GetCardSO().GetCardName(), left.GetCardSO().GetCardName(), StringComparison.Ordinal);
            }
            return costComparison;
        });
    }

    private void SortByNameIncreasing()
    {
        CardImageList.Sort((left, right) => left.GetCardSO().GetCardName().CompareTo(right.GetCardSO().GetCardName()));
    }

    private void SortByNameDecreasing()
    {
        CardImageList.Sort((left, right) => right.GetCardSO().GetCardName().CompareTo(left.GetCardSO().GetCardName()));
    }
}
