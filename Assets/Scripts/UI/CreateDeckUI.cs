using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CreateDeckUI : MonoBehaviour
{
    [SerializeField] private Transform createDeckContent;
    [SerializeField] private GameObject warning;
    [SerializeField] private TMP_InputField cardNameInputField;
    [SerializeField] private Button saveAndGoBackButton;

    private Deck deck;

    private void OnEnable()
    {
        deck = new Deck()
        {
            deckName = "DECK" + DeckDatabase.Instance.DeckSaveDatas.Count + 1,
            cards = new List<CardSO>(),
            avatar = AvatarDatabase.Instance.GetRandomAvatar(),
            cardBack = CardBackDatabase.Instance.GetRandomCardBack()
        };

        warning.SetActive(false);
    }

    public void LoadEmptyCards()
    {
        List<CardImage> cardImages = new List<CardImage>(12);

        for (int i = 0; i < cardImages.Capacity; i++)
        {
            CardImage cardImage = Instantiate(Prefabs.GetCardImagePrefab(), createDeckContent).GetComponent<CardImage>();
            cardImage.ClearCard();
            cardImages.Add(cardImage);
        }
    }

    public void SaveAndGoBack()
    {
        // SAVE DECK
        DeckDatabase.Instance.SaveDeck(deck);

        // GO BACK DEFAULT VIEW
        DeckBuilderUIManager.Instance.GoBackToDefaultUIView();
    }

    public void UpdateDeckName()
    {
        if(CheckDeckName())
        {
            warning.SetActive(false);
            saveAndGoBackButton.interactable = true;

            deck.deckName = cardNameInputField.text;
        }
    }

    private bool CheckDeckName()
    {
        if (string.IsNullOrEmpty(cardNameInputField.text))
        {
            warning.SetActive(true);
            warning.GetComponent<TMP_Text>().text = "Deck name cannot be empty!";

            saveAndGoBackButton.interactable = false;

            return false;
        }

        foreach (var savedDeck in DeckDatabase.Instance.DeckSaveDatas)
        {
            if (savedDeck.deckName == cardNameInputField.text)
            {
                warning.SetActive(true);
                warning.GetComponent<TMP_Text>().text = "Deck name already exists!";

                saveAndGoBackButton.interactable = false;

                return false;
            }
        }

        return true;
    }

    public void DeleteCreatedDeckAndGoBack()
    {
        // WE DO NOT NEED TO DELETE ANYTHING AS THE DECK IS NOT SAVED

        // SO JUST GO BACK DEFAULT VIEW
        DeckBuilderUIManager.Instance.GoBackToDefaultUIView();
    }
}
