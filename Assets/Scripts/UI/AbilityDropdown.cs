using System.Collections.Generic;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using static UnityEngine.Rendering.GPUSort;

public class AbilityDropdown : MonoBehaviour
{
    private TMP_Dropdown abilityDropdown;

    private void Start()
    {
        abilityDropdown = GetComponent<TMP_Dropdown>();
        PopulateAbilities();
    }

    private void PopulateAbilities()
    {
        abilityDropdown.ClearOptions();

        var options = new List<string>();

        foreach (CardAbilityType ability in System.Enum.GetValues(typeof(CardAbilityType)))
        {
            options.Add(Regex.Replace(ability.ToString(), "(\\B[A-Z])", " $1"));
        }

        abilityDropdown.AddOptions(options);
    }

    public void ClearDropbox()
    {
        abilityDropdown.value = -1;
        abilityDropdown.RefreshShownValue();
    }

    public static List<CardImage> FilterByAbility(List<CardImage> cardImages, int abilityIndex)
    {
        if (abilityIndex < 0)
        {
            return cardImages.FindAll(card => card.gameObject.activeInHierarchy);
        }

        CardAbilityType selectedAbility = (CardAbilityType)abilityIndex;

        var filteredCards = new List<CardImage>();

        foreach (var cardImage in cardImages)
        {
            if (cardImage.gameObject.activeInHierarchy && cardImage.GetCardSO().GetAbilityType() == selectedAbility)
            {
                filteredCards.Add(cardImage);
            }
        }
        return filteredCards;
    }
}
