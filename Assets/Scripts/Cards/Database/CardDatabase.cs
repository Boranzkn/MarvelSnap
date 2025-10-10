using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CardDatabase : MonoBehaviour
{
    public static CardDatabase Instance { get; private set; }

    [SerializeField] private List<CardSO> allCardsSOList;

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

        if (cardDictionary == null)
            cardDictionary = allCardsSOList.ToDictionary(c => c.ID, c => c);
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


    // --------------GETTER METHODS--------------
    public List<CardSO> GetAllCards()
    {
        return allCardsSOList;
    }
}
