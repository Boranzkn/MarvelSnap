using UnityEngine;
using UnityEngine.UI;

public class CardImage : MonoBehaviour
{
    [SerializeField] private Sprite placeholderCardImage;

    private CardSO cardSO;
    private Image cardImage;

    private void Awake()
    {
        cardImage = GetComponent<Image>();
    }


    public void ClearCard()
    {
        cardSO = null;
        cardImage.sprite = placeholderCardImage;

        // Set transparency to indicate it's a placeholder
        Color color = cardImage.color;
        color.a = 0.3f;
        cardImage.color = color;
    }

    // --------------GETTER & SETTER METHODS--------------
    public CardSO GetCardSO()
    {
        return cardSO;
    }

    public void SetCardSO(CardSO card)
    {
        if (card != null)
        {
            cardSO = card;
            cardImage.sprite = cardSO.GetSprite();
        }
        else
        {
            Debug.LogError("CardSO is null.");
        }
    }

    public Image GetCardImage()
    {
        return cardImage;
    }

    public void SetCardImage(Sprite sprite)
    {
        if (sprite != null)
        {
            cardImage.sprite = sprite;
        }
        else
        {
            Debug.LogError("Sprite is null.");
        }
    }
}
