using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// HiScoreElement
/// </summary>
[System.Serializable]
public class HiScoreElement
{
    public string Name;
    public float Score;

    public HiScoreElement(string name, float score)
    {
        Name = name;
        Score = score;
    }
}
