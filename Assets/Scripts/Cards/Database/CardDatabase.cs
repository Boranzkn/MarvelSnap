using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CardDatabase : MonoBehaviour
{
    public static CardDatabase Instance { get; private set; }

    private const string CARD_SO_PATH = "Card SO";

    public List<CardSO> AllCardsSOList { get; private set; }

    private Dictionary<string, CardSO> cardDictionary;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        SetAllCards();

        if (cardDictionary == null)
            cardDictionary = AllCardsSOList.ToDictionary(c => c.ID, c => c);
    }

    public CardSO GetCardSOByID(string id)
    {
        if (cardDictionary.TryGetValue(id, out CardSO card))
        {
            return card;
        }
        Debug.LogWarning($"Card with ID {id} not found in the database.");
        return null;
    }

    private void SetAllCards()
    {
        AllCardsSOList = Resources.LoadAll<CardSO>($"{CARD_SO_PATH}").ToList();
    }
}
