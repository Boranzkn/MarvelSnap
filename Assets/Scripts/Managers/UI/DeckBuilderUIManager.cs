using System.Collections.Generic;
using UnityEngine;

public class DeckBuilderUIManager : MonoBehaviour
{
    public static DeckBuilderUIManager Instance { get; private set; }

    [Header("UI Panels")]
    [SerializeField] private Transform canvas;
    [SerializeField] private AllCardsUI allCardsUI;
    [SerializeField] private GameObject allCardBacksUI;
    [SerializeField] private GameObject allAvatarsUI;
    [SerializeField] private AllDecksUI allDecksUI;
    [SerializeField] private GameObject selectedDeckUI;
    [SerializeField] private CreateDeckUI createDeckUI;

    private DeckImage selectedDeck;

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
    }

    public void GoBackToDefaultUIView()
    {
        allCardsUI.gameObject.SetActive(true);
        allDecksUI.gameObject.SetActive(true);

        allCardBacksUI.SetActive(false);
        allAvatarsUI.SetActive(false);
        selectedDeckUI.SetActive(false);
        createDeckUI.gameObject.SetActive(false);
    }

    public void LoadStarterDeckBuilderData()
    {
        allCardsUI.LoadAllCards();
        allDecksUI.LoadAllDecks();
    }

    public void ShowCreateDeckUI()
    {
        allDecksUI.gameObject.SetActive(false);
        selectedDeckUI.SetActive(false);

        createDeckUI.gameObject.SetActive(true);

        createDeckUI.LoadEmptyCards();
    }

    public void ShowSelectedDeckUI(DeckImage deckImage)
    {
        allDecksUI.gameObject.SetActive(false);
        createDeckUI.gameObject.SetActive(false);

        selectedDeckUI.SetActive(true);

        selectedDeck = deckImage;

        LoadSelectedDeckData();
    }

    private void LoadSelectedDeckData()
    {
        //  Create card images for each card in the deck
        foreach (var id in selectedDeck.GetCardIDs())
        {
            CardSO cardSO = CardDatabase.Instance.GetCardSOByName(id);
            CardImage createdCard = Instantiate(Prefabs.GetCardImagePrefab(), selectedDeckUI.transform).GetComponent<CardImage>();

            createdCard.SetCardSO(cardSO);
        }

        //  Fill remaining slots with empty cards if less than 12 cards
        for (int i = selectedDeck.GetCardIDs().Count; i < 12; i++)
        {
            CardImage emptyCard = Instantiate(Prefabs.GetCardImagePrefab(), selectedDeckUI.transform).GetComponent<CardImage>();
            emptyCard.ClearCard();
        }
    }

    public AllCardsUI GetAllCardsUI()
    {
        return allCardsUI;
    }
}
