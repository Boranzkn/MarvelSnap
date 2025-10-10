using System.Collections.Generic;
using UnityEditor.Overlays;
using UnityEngine;
using UnityEngine.InputSystem;

public class DeckDatabase : MonoBehaviour
{
    public static DeckDatabase Instance { get; private set; }

    private const string DECK_KEY_PREFIX = "PLAYER_DECKS";
    private const string DECK_KEYS = "DECK_KEYS";

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

    // FROM DECK SAVE DATA TO DECK OBJECT
    private Deck FromSaveDataToDeck(DeckSaveData save)
    {
        Deck deck = new Deck();
        deck.deckName = save.deckName;
        deck.avatar = AvatarDatabase.Instance.GetAvatarByName(save.avatar);
        deck.cardBack = CardBackDatabase.Instance.GetCardBackByName(save.cardBack);

        foreach (string id in save.cardIDs)
        {
            CardSO data = CardDatabase.Instance.GetCardSOByID(id);
            if (data != null)
                deck.cards.Add(data);
        }

        return deck;
    }

    // FROM DECK OBJECT TO DECK SAVE DATA
    private DeckSaveData FromDeckToSaveData(Deck deck)
    {
        DeckSaveData save = new DeckSaveData();
        save.deckName = deck.deckName;
        save.avatar = deck.avatar.name;
        save.cardBack = deck.cardBack.name;

        foreach (CardSO card in deck.cards)
            save.cardIDs.Add(card.ID);
        return save;
    }

    public void SaveDeck(Deck deck)
    {
        DeckSaveData saveData = FromDeckToSaveData(deck);
        string key = $"{DECK_KEY_PREFIX}_{saveData.deckName}";
        PlayerPrefs.SetString(key, JsonUtility.ToJson(saveData));
        PlayerPrefs.Save();

        SaveKey(key);
    }

    public void SaveDeck(DeckSaveData deck)
    {
        string key = $"{DECK_KEY_PREFIX}_{deck.deckName}";
        PlayerPrefs.SetString(key, JsonUtility.ToJson(deck));
        PlayerPrefs.Save();

        SaveKey(key);
    }

    private void SaveKey(string key)
    {
        DeckKeysSerializable deckKeysSerializable = GetAllDeckKeys();

        if (!deckKeysSerializable.keys.Contains(key))
        {
            deckKeysSerializable.keys.Add(key);
        }

        string json = JsonUtility.ToJson(deckKeysSerializable);
        PlayerPrefs.SetString(DECK_KEYS, json);
        PlayerPrefs.Save();
    }

    public DeckSaveData GetDeckSaveDataByDeckName(string deckName)
    {
        string key = $"{DECK_KEY_PREFIX}_{deckName}";
        if (!PlayerPrefs.HasKey(key)) return null;

        string json = PlayerPrefs.GetString(key);
        return JsonUtility.FromJson<DeckSaveData>(json);
    }

    public List<DeckSaveData> GetAllDeckSaveDatas()
    {
        List<DeckSaveData> deckSaveDatas = new List<DeckSaveData>();

        if (GetAllDeckKeys().keys.Count == 0) return deckSaveDatas;

        foreach (string key in GetAllDeckKeys().keys)
        {
            if (key.StartsWith(DECK_KEY_PREFIX))
            {
                string json = PlayerPrefs.GetString(key);
                deckSaveDatas.Add(JsonUtility.FromJson<DeckSaveData>(json));
            }
        }
        return deckSaveDatas;
    }

    private DeckKeysSerializable GetAllDeckKeys()
    {
        if (!PlayerPrefs.HasKey(DECK_KEYS)) return new DeckKeysSerializable();

        string json = PlayerPrefs.GetString(DECK_KEYS);
        return JsonUtility.FromJson<DeckKeysSerializable>(json);
    }
}
