using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static GameOverMenuControls;

public class MenuControls : MonoBehaviour
{
    public enum MenuOption { Play, Highscore, Options, Exit, Back, Graphics, Resolution, Volume, Fullscreen }

    protected virtual MenuOption CurrentOption { get; set; }

    public GameObject playButton;
    public GameObject highscoreButton;
    public GameObject optionsButton;
    public GameObject exitButton;
    public GameObject backButton;

    public GameObject mainMenu;
    public GameObject optionsMenu;

    public TMP_Dropdown graphicsDropdown;
    public TMP_Dropdown resolutionDropdown;
    public GameObject volumeSlider;
    public GameObject fullscreenButton;

    private GameObject highlightedButton;

    //private bool playButtonHighlighted = false;
    private bool backButtonHighlighted = false;
    private bool isGameOverSceneLoaded = false;

    protected virtual void SetCurrentOption(MenuOption option)
    {
        CurrentOption = option;
    }

    private GameObject retryScreen;

    protected virtual void Start()
    {
        CurrentOption = MenuOption.Play;
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "GameOver")
        {
            retryScreen = GameObject.Find("RetryScreen");
            isGameOverSceneLoaded = true;
        }
        else
        {
            isGameOverSceneLoaded = false;
        }
    }

    protected virtual void Update()
    {
        if (SceneManager.GetSceneByName("Menu").isLoaded && mainMenu.activeSelf)
        {
            HandleMainMenuNavigation();
        }
        else if (SceneManager.GetSceneByName("Menu").isLoaded && optionsMenu.activeSelf)
        {
            HandleOptionsMenuNavigation();
        }
        else if (SceneManager.GetSceneByName("HighScoreTable").isLoaded)
        {
            if (!backButtonHighlighted)
            {
                HighlightButton(backButton);
                backButtonHighlighted = true;
            }

            if (Input.GetKeyDown(KeyCode.Return))
            {
                SimulateButtonClick();
            }
        }
        if (SceneManager.GetSceneByName("GameOver").isLoaded && retryScreen != null && retryScreen.activeSelf)
        {
            Debug.Log("Entered gameoverscene");
            GameOverMenuControls gameOverMenu = FindObjectOfType<GameOverMenuControls>();
            if (gameOverMenu != null)
            {
                gameOverMenu.HandleMainMenuNavigation();
                HighlightButton(gameOverMenu.quitButton);
            }
        }

    }

    protected virtual void HandleMainMenuNavigation()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            ChangeOption(MenuOption.Play, MenuOption.Exit, MenuOption.Options, MenuOption.Highscore);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            ChangeOption(MenuOption.Exit, MenuOption.Play, MenuOption.Highscore, MenuOption.Options);

        }
        else if (Input.GetKeyDown(KeyCode.Return))
        {
            SimulateButtonClick();
        }
    }

    protected virtual void HandleOptionsMenuNavigation()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            ChangeOption(MenuOption.Back, MenuOption.Volume, MenuOption.Resolution, MenuOption.Graphics, MenuOption.Fullscreen);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            ChangeOption(MenuOption.Fullscreen, MenuOption.Graphics, MenuOption.Resolution, MenuOption.Volume, MenuOption.Back);

        }
        else if (Input.GetKeyDown(KeyCode.Return))
        {
            SimulateButtonClick();
        }
    }

    protected virtual void ChangeOption(params MenuOption[] options)
    {
        int index = Array.IndexOf(options, CurrentOption);
        if (index == -1)
        {
            CurrentOption = options[0];
        }
        else
        {
            CurrentOption = options[(index + 1) % options.Length];
        }

        HighlightButton(GetCurrentButton());
    }

    protected virtual void HighlightButton(GameObject button)
    {
        Debug.Log("Highlighting: " + (button != null ? button.name : "null"));
        highlightedButton = button;

        // Simulate mouse hover by selecting the button
        UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject(button);
    }
    protected virtual void SimulateButtonClick()
    {
        Button buttonComponent = highlightedButton?.GetComponent<Button>();
        if (buttonComponent != null)
        {
            buttonComponent.onClick.Invoke();
        }
        else
        {
            Debug.LogError("The selected GameObject does not have a Button component.");
        }
    }

    protected virtual GameObject GetCurrentButton()
    {
        switch (CurrentOption)
        {
            case MenuOption.Play:
                return playButton;
            case MenuOption.Highscore:
                return highscoreButton;
            case MenuOption.Options:
                return optionsButton;
            case MenuOption.Exit:
                return exitButton;
            case MenuOption.Back:
                return backButton;
            case MenuOption.Graphics:
                return graphicsDropdown.gameObject;
            case MenuOption.Resolution:
                return resolutionDropdown.gameObject;
            case MenuOption.Fullscreen:
                return fullscreenButton.gameObject;
            case MenuOption.Volume:
                return volumeSlider;
            default:
                return null;
        }
    }
}
