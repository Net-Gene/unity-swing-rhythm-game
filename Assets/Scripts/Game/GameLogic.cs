using System.Collections;
using TMPro;
using UnityEngine;

public class GameLogic : MonoBehaviour
{
    private float time;
    public static int score;
    public TMP_Text gameScoreText;
    public AnimationClip scoreChangeAnimationClip;
    public Animation animation;

    private int currentScore;  // Nykyinen pistemäärä

    private void Start()
    {
        time = Time.time;
        currentScore = GameLogic.score;  // Alustetaan nykyinen pistemäärä pelin alussa
    }

    private void Update()
    {
        // Tarkista, onko pistemäärä muuttunut
        if (currentScore != GameLogic.score)
        {
            // Käynnistä animaatio käyttäen AnimationClip:iä
            animation.clip = scoreChangeAnimationClip;
            animation.Play();
        }

        // Päivitä pistemääräteksti
        gameScoreText.text = GameLogic.score.ToString();
    }
}
