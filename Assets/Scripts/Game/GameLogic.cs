using System.Collections;
using TMPro;
using UnityEngine;

public class GameLogic : MonoBehaviour
{
    public static int score;
    public TMP_Text gameScoreText;
 

    private int currentScore;  // Nykyinen pistemäärä

    private void Start()
    {
        currentScore = GameLogic.score;  // Alustetaan nykyinen pistemäärä pelin alussa
    }

    private void Update()
    {
        // Päivitä pistemääräteksti
        gameScoreText.text = GameLogic.score.ToString();
    }
}
