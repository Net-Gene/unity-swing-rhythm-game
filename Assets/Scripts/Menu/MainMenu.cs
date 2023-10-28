using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour
{
    // Metodi pelin pelaamiseen
    public void PlayGame()
    {
        // Lataa seuraava peliskenaario (+1 nykyisestä skenaarionumerosta)
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    // Metodi pelin sulkemiseen
    public void QuitGame()
    {
        // Lopettaa sovelluksen suorituksen
        Application.Quit();
    }
}
