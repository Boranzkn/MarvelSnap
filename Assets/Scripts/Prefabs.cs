using UnityEngine;

public static class Prefabs
{
    private static GameObject cardImagePrefab = Resources.Load<GameObject>("Prefabs/Images/Card Image");
    private static GameObject deckImagePrefab = Resources.Load<GameObject>("Prefabs/Images/Deck Image");

    public static GameObject GetCardImagePrefab() => cardImagePrefab;
    public static GameObject GetDeckImagePrefab() => deckImagePrefab;
}
