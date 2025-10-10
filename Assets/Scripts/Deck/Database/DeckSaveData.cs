using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DeckSaveData
{
    public string deckName;
    public string avatar;
    public string cardBack;
    public List<string> cardIDs = new List<string>(12);
}