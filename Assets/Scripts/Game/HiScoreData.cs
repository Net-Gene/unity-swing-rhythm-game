using System.Collections.Generic;

[System.Serializable]
public class HiScoreData
{
    public List<HiScoreElement> HiScoreElementList;

    public HiScoreData(List<HiScoreElement> list)
    {
        HiScoreElementList = list;
    }
}
