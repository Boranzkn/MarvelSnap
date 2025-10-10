using UnityEngine;

public class CardBackDatabase : MonoBehaviour
{
    public static CardBackDatabase Instance { get; private set; }

    private const string CARD_BACKS_PATH = "Card Backs";

    public Sprite[] CardBacks { get; private set; }

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
        SetCardBacks();
    }

    private void SetCardBacks()
    {
        CardBacks = Resources.LoadAll<Sprite>(CARD_BACKS_PATH);
        if (CardBacks == null || CardBacks.Length == 0)
        {
            Debug.LogWarning("No card backs found in Resources/Card Backs.");
        }
    }

    public Sprite GetCardBackByName(string name)
    {
        return Resources.Load<Sprite>($"{CARD_BACKS_PATH}/{name}");
    }
}
