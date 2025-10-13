using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class CardDescription : MonoBehaviour
{
    public static CardDescription Instance { get; private set; }

    public const string TAG_SHOW_DESCRIPTION = "ShowDescription";

    [SerializeField] private TextMeshProUGUI cardDescriptionText;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            gameObject.SetActive(false);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void DoAllOnSelectAnimations(string text, RectTransform rectTransform)
    {
        cardDescriptionText.text = text;
        SetTransform(rectTransform);
        gameObject.SetActive(true);
        transform.DOScale(1.1f, 0.2f).SetEase(Ease.OutBack);
    }

    public void DoAllOnDeselectAnimations()
    {
        transform.DOScale(1f, 0.2f).SetEase(Ease.OutBack);
        gameObject.SetActive(false);
    }

    private void SetTransform(RectTransform cardRect, float verticalOffset = 0f)
    {
        Canvas.ForceUpdateCanvases();

        Canvas canvas = GetComponentInParent<Canvas>();
        if (canvas == null)
        {
            Debug.LogWarning("No parent Canvas found for description.");
            return;
        }

        RectTransform descRect = GetComponent<RectTransform>();
        RectTransform canvasRect = canvas.GetComponent<RectTransform>();
        Camera cam = (canvas.renderMode == RenderMode.ScreenSpaceOverlay) ? null : canvas.worldCamera;

        // Card position in world and screen space
        Vector3 cardWorldCenter = cardRect.TransformPoint(cardRect.rect.center);
        Vector2 cardScreenPoint = RectTransformUtility.WorldToScreenPoint(cam, cardWorldCenter);

        // Convert to canvas local position
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRect, cardScreenPoint, cam, out Vector2 cardLocalPos);

        // Compute space below card in screen space
        float cardBottomScreenY = cardScreenPoint.y - cardRect.rect.height / 2 * (Screen.height / canvasRect.rect.height);
        float spaceBelow = cardBottomScreenY; // distance from bottom of card to bottom of screen
        float descHeightScreen = descRect.rect.height * (Screen.height / canvasRect.rect.height);

        // Decide placement: prefer below if enough space, otherwise above
        float finalOffsetY;
        if (spaceBelow >= descHeightScreen)
            finalOffsetY = -cardRect.rect.height / 2 - descRect.rect.height / 2 + verticalOffset; // below
        else
            finalOffsetY = cardRect.rect.height / 2 + descRect.rect.height / 2 + verticalOffset;  // above

        // Set final anchored position
        descRect.anchoredPosition = cardLocalPos + new Vector2(0, finalOffsetY);
    }
}
