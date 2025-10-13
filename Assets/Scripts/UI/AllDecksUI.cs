using UnityEngine;

public class AllDecksUI : MonoBehaviour
{
    [SerializeField] private Transform allDecksContent;

    public void LoadAllDecks()
    {
        foreach (var deckSaveData in DeckDatabase.Instance.DeckSaveDatas)
        {
            CreateDeckImage(deckSaveData);
        }
    }

    private void CreateDeckImage(DeckSaveData saveData)
    {
        DeckImage deckImage = Instantiate(Prefabs.GetDeckImagePrefab(), allDecksContent).GetComponent<DeckImage>();
        deckImage.Initialize(saveData);
    }
}
