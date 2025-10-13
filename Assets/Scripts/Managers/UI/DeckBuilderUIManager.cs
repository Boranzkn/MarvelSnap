using System.Collections.Generic;
using UnityEngine;

public class DeckBuilderUIManager : MonoBehaviour
{
    public static DeckBuilderUIManager Instance { get; private set; }

    [Header("UI Panels")]
    [SerializeField] private Transform canvas;
    [SerializeField] private GameObject createDeckUI;
    [SerializeField] private AllCardsUI allCardsUI;
    [SerializeField] private AllDecksUI allDecksUI;
    [SerializeField] private GameObject selectedDeckUI;

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

    public void LoadStarterDeckBuilderData()
    {
        allCardsUI.LoadAllCards();
        allDecksUI.LoadAllDecks();
    }

    public void ShowCreateDeckUI()
    {
        allDecksUI.gameObject.SetActive(false);
        selectedDeckUI.SetActive(false);

        createDeckUI.SetActive(true);

        LoadEmptyCards();
    }

    private void LoadEmptyCards()
    {
        List<CardImage> cardImages = new List<CardImage>(12);

        for (int i = 0; i < cardImages.Capacity; i++)
        {
            CardImage cardImage = Instantiate(Prefabs.GetCardImagePrefab(), createDeckUI.transform).GetComponent<CardImage>();
            cardImage.ClearCard();
            cardImages.Add(cardImage);
        }
    }

    public void ShowSelectedDeckUI(DeckImage deckImage)
    {
        allDecksUI.gameObject.SetActive(false);
        createDeckUI.SetActive(false);

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
}
