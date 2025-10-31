using UnityEngine;

public class SortToggle : MonoBehaviour
{
    [SerializeField] private RectTransform arrow;

    public void OnToggleChanged(bool isOn)
    {
        if (isOn)
        {
            arrow.localRotation = Quaternion.Euler(0, 0, 0); // Pointing down
        }
        else
        {
            arrow.localRotation = Quaternion.Euler(0, 0, 180); // Pointing up
        }

        DeckBuilderUIManager.Instance.GetAllCardsUI().SortAndDisplayCards();
    }
}
