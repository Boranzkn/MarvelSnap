using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CardDatabase : MonoBehaviour
{
    public static CardDatabase Instance { get; private set; }

    private const string CARD_SO_PATH = "Card SO";

    public List<CardSO> AllCardsSOList { get; private set; }

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
    }

    public CardSO GetCardSOByName(string name)
    {
        return Resources.Load<CardSO>($"{CARD_SO_PATH}/{name}");
    }

    private void SetAllCards()
    {
        AllCardsSOList = Resources.LoadAll<CardSO>($"{CARD_SO_PATH}").ToList();
    }
}
