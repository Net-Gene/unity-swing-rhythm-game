using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathMenu : MonoBehaviour
{
    // Uudelleen yritys
    public void Retry()
    {
        // Ladataan Game scenario
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void MainMenu()
    {
        // Ladataan MainMenu scenario
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 2);
    }

    // Suljetaan peli
    public void QuitGame()
    {
        // Lopettaa sovelluksen suorituksen
        Application.Quit();
    }
}
