using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NykyinenScore : MonoBehaviour
{
    public float score = 0;

    // Lis‰t‰‰n tai v‰hennet‰‰n pisteit‰
    public void LisaaPisteita(float maara)
    {
        score += maara;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        // Nollataan pisteet pelin aluksi
        score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
