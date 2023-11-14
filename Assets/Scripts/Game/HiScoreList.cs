using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// HiScoreList
/// </summary>
[System.Serializable]
public class HiScoreList
{ 

    /// <summary>
    /// hiScoreElementList
    /// </summary>
    public List<HiScoreElement> hiScoreElementList;

    public List<HiScoreElement> HiScoreElementList
    {
        get
        {
            return this.hiScoreElementList;
        }
        set
        {
            this.hiScoreElementList = value;
        }
    }



    /// <summary>
    /// AddToList
    /// </summary>
    /// <param name="element"></param>
    public void AddToList(HiScoreElement element) {

        if (hiScoreElementList.Count == 0)
        {
            hiScoreElementList.Add(element);
            return;
        }
        int counter = 0;
        foreach(HiScoreElement el in hiScoreElementList)
        {
            if (el.Score < element.Score)
            {
                hiScoreElementList.Insert(counter, element);
                break;
            }
            counter++;

            if (hiScoreElementList.Count == counter)
            {
                hiScoreElementList.Add(element);
                break;
            }
        }
        if(hiScoreElementList.Count > 10)
        {
            hiScoreElementList.RemoveAt(10);
        }
        
    }


}
