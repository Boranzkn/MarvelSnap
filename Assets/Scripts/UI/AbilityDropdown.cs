using System.Collections.Generic;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;

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

    public void ClearDropdown()
    {
        abilityDropdown.value = -1;
        abilityDropdown.RefreshShownValue();
    }
}
