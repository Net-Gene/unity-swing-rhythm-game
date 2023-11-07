using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class DeathMenu : MonoBehaviour
{
    public HighScoreTable highScoreTable; // Viittaus HighScoreTable-skriptiin
    public TMP_InputField InputNameField;  // InputNameField (syötekenttä) valintapudotusvalikko

    // Uudelleen yritys
    public void Retry()
    {
        // Ladataan Game-scenarion
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void MainMenu()
    {
        // Ladataan MainMenu-scenarion
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 2);
    }

    // Suljetaan peli
    public void QuitGame()
    {
        // Lopettaa sovelluksen suorituksen
        Application.Quit();
    }

    public void SaveName()
    {
        // Haetaan pelaajan nimi syötekentästä
        string playerName = InputNameField.text;
        PlayerPrefs.SetString("Name", playerName);

        // Haetaan pelaajan nykyinen pisteet PlayerPrefsista
        int score = PlayerPrefs.GetInt("CurrentScore", 0);

        // Voit nyt kutsua HighScoreTable-skriptin metodia pisteiden ja nimen lisäämiseksi
        highScoreTable.AddNewScore(playerName, score);
    }
}
