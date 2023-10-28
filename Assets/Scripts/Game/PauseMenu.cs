using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    // N�yt� pause-valikon kanvaasi aluksi piilotettuna.
    public GameObject pauseMenuCanvas;
    private bool isPaused = false;

    private void Start()
    {
        // Aluksi piilota pause-valikon kanvaasi.
        if (pauseMenuCanvas != null)
        {
            pauseMenuCanvas.SetActive(false);
        }
    }

    private void Update()
    {
        // Tarkista "Escape"-n�pp�imen painallus pause-valikon tilan vaihtamiseksi.
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void PauseGame()
    {
        // N�yt� pause-valikon kanvaasi ja keskeyt� peli.
        if (pauseMenuCanvas != null)
        {
            pauseMenuCanvas.SetActive(true);
            Time.timeScale = 0f; // Pys�yt� peliaika.
            isPaused = true;
        }
    }

    public void ResumeGame()
    {
        // Piilota pause-valikon kanvaasi ja jatka peli�.
        if (pauseMenuCanvas != null)
        {
            pauseMenuCanvas.SetActive(false);
            Time.timeScale = 1f; // Jatka normaalia peliaikaa.
            isPaused = false;
        }
    }

    public void GoToMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
