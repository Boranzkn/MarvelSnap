using System.Collections.Generic;
using UnityEngine;

public class Hand : CardPlacer
{
    public static Hand Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }
}
