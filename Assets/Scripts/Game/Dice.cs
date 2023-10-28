using UnityEngine;
using TMPro;

public class Dice : MonoBehaviour
{
    public TMPro.TMP_Text score;   // Pistem‰‰r‰-teksti
    public TMPro.TMP_Text highScore;   // Huippupistem‰‰r‰-teksti

    private int currentScore = 0; // Store the current score

    private void Start()
    {
        // Asetetaan alkuper‰inen huippupistem‰‰r‰ tallennetuista tiedoista
        highScore.text = PlayerPrefs.GetInt("Highscore", 0).ToString();
    }

    public void RollDice()
    {
        // Arvotaan satunnainen numero 1 ja 6 v‰lill‰
        int number = Random.Range(1, 7);

        // P‰ivitet‰‰n n‰ytett‰v‰ pistem‰‰r‰
        currentScore = number; // Update the score
        score.text = currentScore.ToString();

        // Tarkistetaan, onko saavutettu pistem‰‰r‰ uusi huippupistem‰‰r‰
        if (currentScore > PlayerPrefs.GetInt("Highscore", 0))
        {
            // P‰ivitet‰‰n huippupistem‰‰r‰ tallennetuksi uudeksi enn‰tykseksi
            PlayerPrefs.SetInt("Highscore", currentScore);

            // P‰ivitet‰‰n huippupistem‰‰r‰-teksti
            highScore.text = currentScore.ToString();
        }

        // Save the current score
        PlayerPrefs.SetInt("CurrentScore", currentScore);
    }

    public int GetScore()
    {
        return currentScore; // Provide a method to access the score
    }

    public void Reset()
    {
        // Poistetaan tallennettu huippupistem‰‰r‰
        PlayerPrefs.DeleteKey("Highscore");
        highScore.text = "0";

        // Reset the current score
        currentScore = 0;
        score.text = "0";

        // Delete the saved current score
        PlayerPrefs.DeleteKey("CurrentScore");
    }
}
