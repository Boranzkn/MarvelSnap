using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DeckImage : MonoBehaviour
{
    private List<string> cardIDs;
    private Button button;

    [SerializeField] private Image avatar;
    [SerializeField] private Image cardBack;
    [SerializeField] private TextMeshProUGUI deckNameText;

    private void Awake()
    {
        button = GetComponent<Button>();

        button.onClick.AddListener(() =>
        {
            DeckBuilderUIManager.Instance.ShowSelectedDeckUI(this);
        });
    }
    
    public void Initialize(DeckSaveData saveData)
    {
        deckNameText.text = saveData.deckName;
        avatar.sprite = AvatarDatabase.Instance.GetAvatarByName(saveData.avatar);
        cardBack.sprite = CardBackDatabase.Instance.GetCardBackByName(saveData.cardBack);
        cardIDs = saveData.cardIDs;
    }

    public List<string> GetCardIDs() => cardIDs;
}
