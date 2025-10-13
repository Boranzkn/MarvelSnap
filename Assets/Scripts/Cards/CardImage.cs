using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardImage : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, ISelectHandler, IDeselectHandler
{
    [SerializeField] private Sprite placeholderCardImage;

    private CardSO cardSO;
    private Image cardImage;
    private RectTransform rectTransform;

    private void Awake()
    {
        cardImage = GetComponent<Image>();
        rectTransform = GetComponent<RectTransform>();
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
            SetCardImage(card.GetSprite());
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

    public void OnPointerEnter(PointerEventData eventData)
    {
        eventData.selectedObject = gameObject;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        eventData.selectedObject = null;
    }

    public void OnSelect(BaseEventData eventData)
    {
        transform.DOScale(1.1f, 0.2f).SetEase(Ease.OutBack);

        if (cardSO != null && eventData.selectedObject.CompareTag(CardDescription.TAG_SHOW_DESCRIPTION))
        {
            CardDescription.Instance.DoAllOnSelectAnimations(cardSO.GetDescriptionWithAbility(), rectTransform);
        }
    }

    public void OnDeselect(BaseEventData eventData)
    {
        transform.DOScale(1f, 0.2f).SetEase(Ease.OutBack);
        CardDescription.Instance.DoAllOnDeselectAnimations();
    }
}
