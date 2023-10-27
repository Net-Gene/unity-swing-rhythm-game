using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuCanvas;
    private bool isPaused = false;

    private void Start()
    {
        // Initially, hide the pause menu canvas.
        if (pauseMenuCanvas != null)
        {
            pauseMenuCanvas.SetActive(false);
        }
    }

    private void Update()
    {
        // Check for the "Escape" key to toggle the pause menu.
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
        // Show the pause menu canvas and pause the game.
        if (pauseMenuCanvas != null)
        {
            pauseMenuCanvas.SetActive(true);
            Time.timeScale = 0f; // Pause the game time.
            isPaused = true;
        }
    }

    public void ResumeGame()
    {
        // Hide the pause menu canvas and resume the game.
        if (pauseMenuCanvas != null)
        {
            pauseMenuCanvas.SetActive(false);
            Time.timeScale = 1f; // Resume normal game time.
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
