using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameLogic : MonoBehaviour
{
    public static int score;
    public TMP_Text gameScoreText;
 

    private int currentScore;  // Nykyinen pistemäärä

    private void Start()
    {
        // Check if the scene named "game" is active and enabled
        Scene gameScene = SceneManager.GetSceneByName("Game");
        if (gameScene.isLoaded)
        {
            GameLogic.score = 0;
        }

        currentScore = GameLogic.score;  // Alustetaan nykyinen pistemäärä pelin alussa
    }

    private void Update()
    {
        // Käsittele Pause-valikon navigointia
        if (SceneManager.GetSceneByName("Game").isLoaded)
        {
            gameScoreText.text = GameLogic.score.ToString();
        }
        // Päivitä pistemääräteksti
    }
}
