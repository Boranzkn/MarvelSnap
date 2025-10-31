using System.Collections.Generic;

public static class SearchBar
{
    public static List<CardImage> ApplySearchFilter(List<CardImage> cards, string searchText)
    {
        if (string.IsNullOrEmpty(searchText)) return cards;

        searchText.ToLower();
        return cards.FindAll(card => card.GetCardSO().GetCardName().ToLower().Contains(searchText));
    }
}
