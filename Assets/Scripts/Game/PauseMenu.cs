using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    // Näytä pause-valikon kanvaasi aluksi piilotettuna.
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
        // Tarkista "Escape"-näppäimen painallus pause-valikon tilan vaihtamiseksi.
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
        // Näytä pause-valikon kanvaasi ja keskeytä peli.
        if (pauseMenuCanvas != null)
        {
            pauseMenuCanvas.SetActive(true);
            Time.timeScale = 0f; // Pysäytä peliaika.
            isPaused = true;
        }
    }

    public void ResumeGame()
    {
        // Piilota pause-valikon kanvaasi ja jatka peliä.
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
