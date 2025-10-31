using System;
using System.Collections.Generic;

public static class CardImageSorter
{
    public static void SortCards(List<CardImage> cards, SortType sortType, bool ascending)
    {
        Comparison<CardImage> comparison = sortType switch
        {
            SortType.Cost => (a, b) => 
            { 
                int costComparison = a.GetCardSO().GetCost().CompareTo(b.GetCardSO().GetCost());
                if (costComparison == 0)
                {
                    // If costs are the same, compare by name
                    return string.Compare(a.GetCardSO().GetCardName(), b.GetCardSO().GetCardName(), StringComparison.Ordinal);
                }
                return costComparison;
            },

            SortType.Power => (a, b) =>
            { 
                int powerComparison = a.GetCardSO().GetPower().CompareTo(b.GetCardSO().GetPower());
                if (powerComparison == 0)
                {
                    // If powers are the same, compare by name
                    return string.Compare(a.GetCardSO().GetCardName(), b.GetCardSO().GetCardName(), StringComparison.Ordinal);
                }
                return powerComparison;
            },

            SortType.Name => (a, b) => string.Compare(a.GetCardSO().GetCardName(), b.GetCardSO().GetCardName(), StringComparison.Ordinal),
            _ => null
        };

        if (comparison == null) return;

        if (ascending)
            cards.Sort(comparison);
        else
            cards.Sort((a, b) => comparison(b, a));
    }
}
