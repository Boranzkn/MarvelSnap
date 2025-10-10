using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour
{
    public List<CardSO> cards = new List<CardSO>(12);
    public string deckName;
    public Sprite avatar;
    public Sprite cardBack;
}
