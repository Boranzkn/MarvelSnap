using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeckBuilderUIManager : MonoBehaviour
{
    public static DeckBuilderUIManager Instance { get; private set; }
    
    public const string TAG_SHOW_DESCRIPTION = "ShowDescription";

    public CardDescription CardDescription;

    [Header("UI Panels")]
    [SerializeField] private Transform canvas;
    [SerializeField] private GameObject createDeckUI;
    [SerializeField] private GameObject allDecksUI;
    [SerializeField] private GameObject selectedDeckUI;

    [Header("Card Prefab and Content Holder")]
    [SerializeField] private Image cardImagePrefab;
    [SerializeField] private Transform allCardsContent;

    [Header("Deck Prefab and Content Holder")]
    [SerializeField] private GameObject deckImagePrefab;
    [SerializeField] private Transform allDecksContent;

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

    private void Start()
    {
        CardDescription = Instantiate(CardDescription, canvas);
    }

    public void ShowCreateDeckUI()
    {
        allDecksUI.SetActive(false);
        selectedDeckUI.SetActive(false);

        createDeckUI.SetActive(true);

        LoadEmptyCards();
    }

    private void LoadEmptyCards()
    {
        List<CardImage> cardImages = new List<CardImage>(12);

        for (int i = 0; i < cardImages.Capacity; i++)
        {
            CardImage cardImage = Instantiate(cardImagePrefab, createDeckUI.transform).GetComponent<CardImage>();
            cardImage.ClearCard();
            cardImages.Add(cardImage);
        }
    }

    public void ShowSelectedDeckUI(DeckImage deckImage)
    {
        allDecksUI.SetActive(false);
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
            CardImage createdCard = Instantiate(cardImagePrefab, selectedDeckUI.transform).GetComponent<CardImage>();

            createdCard.SetCardSO(cardSO);
        }

        //  Fill remaining slots with empty cards if less than 12 cards
        for (int i = selectedDeck.GetCardIDs().Count; i < 12; i++)
        {
            CardImage emptyCard = Instantiate(cardImagePrefab, selectedDeckUI.transform).GetComponent<CardImage>();
            emptyCard.ClearCard();
        }
    }

    public void LoadAllCards()
    {
        foreach (var cardSO in CardDatabase.Instance.AllCardsSOList)
        {
            CardImage createdCard = Instantiate(cardImagePrefab, allCardsContent).GetComponent<CardImage>();
            createdCard.gameObject.tag = TAG_SHOW_DESCRIPTION;

            createdCard.SetCardSO(cardSO);
        }
    }

    public void LoadAllDecks()
    {
        foreach (var deckSaveData in DeckDatabase.Instance.DeckSaveDatas)
        {
            CreateDeckImage(deckSaveData);
        }
    }

    private void CreateDeckImage(DeckSaveData saveData)
    {
        DeckImage deckImage = Instantiate(deckImagePrefab, allDecksContent).GetComponent<DeckImage>();
        deckImage.Initialize(saveData);
    }
}
