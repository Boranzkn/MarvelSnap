using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeckBuilderUIManager : MonoBehaviour
{
    public static DeckBuilderUIManager Instance { get; private set; }

    //  UI
    [SerializeField] private GameObject createDeckUI;
    [SerializeField] private GameObject allDecksUI;
    [SerializeField] private GameObject selectedDeckUI;

    //  LOAD ALL CARDS UI
    [SerializeField] private Image cardImagePrefab;
    [SerializeField] private Transform allCardsContent;

    //  LOAD ALL DECKS UI
    [SerializeField] private GameObject deckImagePrefab;
    [SerializeField] private Transform allDecksContent;

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

        LoadSelectedDeckData(deckImage);
    }

    private void LoadSelectedDeckData(DeckImage deckImage)
    {
        //  Create card images for each card in the deck
        foreach (var id in deckImage.GetCardIDs())
        {
            CardSO cardSO = CardDatabase.Instance.GetCardSOByName(id);
            CardImage createdCard = Instantiate(cardImagePrefab, selectedDeckUI.transform).GetComponent<CardImage>();

            createdCard.SetCardSO(cardSO);
        }

        //  Fill remaining slots with empty cards if less than 12 cards
        for (int i = deckImage.GetCardIDs().Count; i < 12; i++)
        {
            CardImage emptyCard = Instantiate(cardImagePrefab, selectedDeckUI.transform).GetComponent<CardImage>();
            emptyCard.ClearCard();
        }
    }

    public void LoadAllCards()
    {
        foreach (var cardSO in CardDatabase.Instance.AllCardsSOList)
        {
            Image createdCard = Instantiate(cardImagePrefab, allCardsContent);

            createdCard.sprite = cardSO.GetSprite();
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
