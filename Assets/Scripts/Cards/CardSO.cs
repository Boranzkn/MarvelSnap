using System.Text.RegularExpressions;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Card")]
public class CardSO : ScriptableObject
{
    [SerializeField] private string cardName;
    [SerializeField] private string description;
    [SerializeField] private CardAbilityType abilityType;
    [SerializeField] private int cost;
    [SerializeField] private int power;
    [SerializeField] private Sprite sprite;



    // --------------GETTER METHODS--------------
    public string GetCardName()
    {
        return cardName;
    }

    public string GetDescriptionWithAbility()
    {
        if (abilityType == CardAbilityType.NoAbility || abilityType == CardAbilityType.SpecificText)
        {
            return description;
        }

        return Regex.Replace(abilityType.ToString(), "(\\B[A-Z])", " $1") + ": " + description;
    }

    public CardAbilityType GetAbilityType()
    {
        return abilityType;
    }

    public int GetCost()
    {
        return cost;
    }

    public int GetPower()
    {
        return power;
    }

    public Sprite GetSprite()
    {
        return sprite;
    }
}
