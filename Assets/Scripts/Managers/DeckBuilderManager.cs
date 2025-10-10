using UnityEngine;

public class DeckBuilderManager : MonoBehaviour
{
    public static DeckBuilderManager Instance { get; private set; }

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
        DeckBuilderUIManager.Instance.LoadAllCards();
        DeckBuilderUIManager.Instance.LoadAllDecks();
    }
}