using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// HiScoreElement
/// </summary>
[System.Serializable]
public class HiScoreElement
{
    // Pelaajan nimi
    public string Name;

    // Pistemäärä
    public float Score = GameLogic.score;

    // Konstruktori
    public HiScoreElement(string name, float score)
    {
        Name = name;
        Score = score;
    }
}
