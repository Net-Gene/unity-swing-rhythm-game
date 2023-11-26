using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static GameOverMenuControls;

public class MenuControls : MonoBehaviour
{
    public enum MenuOption { Play, Highscore, Options, Exit, Back, Graphics, Resolution, Volume }

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

    private GameObject highlightedButton;

    private const string BackButtonName = "BackButton";

    protected virtual void SetCurrentOption(MenuOption option)
    {
        CurrentOption = option;
    }

    protected virtual void Start()
    {
        CurrentOption = MenuOption.Play;
        HighlightButtonBasedOnScene();
    }

    void Update()
    {
        HandleMenuNavigation();
        SimulateButtonClick();
    }

    protected virtual void HighlightButtonBasedOnScene()
    {
        int activeSceneIndex = SceneManager.GetActiveScene().buildIndex;

        if (activeSceneIndex == 0)
        {
            HighlightButton(playButton);
        }
        else if (activeSceneIndex == 1)
        {
            HighlightButton(null);
        }
        else if (activeSceneIndex == 2)
        {
            StartCoroutine(WaitForRetryScreen());
        }
        else if (activeSceneIndex == 3)
        {
            HighlightButton(backButton);
        }
        // Add more conditions as needed
    }

    protected virtual IEnumerator WaitForRetryScreen()
    {
        GameObject retryScreen = GameObject.Find("RetryScreen");

        while (retryScreen == null || !retryScreen.activeSelf)
        {
            yield return null;
        }

        // Execute your logic after the optionsMenu is active
        GameOverMenuControls gameOverMenu = FindObjectOfType<GameOverMenuControls>();
        if (gameOverMenu != null)
        {
            gameOverMenu.ChangeOption((MenuOption)GameOverMenuOption.Quit,
                                      (MenuOption)GameOverMenuOption.Retry,
                                      (MenuOption)GameOverMenuOption.MainMenu);
            CurrentOption = MenuOption.Play;
        }
    }

    protected virtual void HandleMenuNavigation()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            MoveSelectionUp();
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            MoveSelectionDown();
        }
        else if (Input.GetKeyDown(KeyCode.Return))
        {
            SimulateButtonClick();
            if (CurrentOption == MenuOption.Options)
            {
                NavigateOptionsSubmenu();
            }
        }
    }

    protected virtual void MoveSelectionUp() 
        => ChangeOption(MenuOption.Play, MenuOption.Exit, MenuOption.Options, MenuOption.Highscore);

    protected virtual void MoveSelectionDown()
        => ChangeOption(MenuOption.Exit, MenuOption.Play, MenuOption.Highscore, MenuOption.Options);
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

    protected virtual void NavigateOptionsSubmenu()
    {
        // Handle navigation within the options menu
        ChangeOption(MenuOption.Graphics, MenuOption.Resolution, MenuOption.Volume, MenuOption.Back);
    }

    protected virtual void HighlightButton(GameObject button)
    {
        Debug.Log("Highlighting: " + (button != null ? button.name : "null"));
        highlightedButton = button;
    }

    protected virtual void SimulateButtonClick()
    {
        if (Input.GetKeyDown(KeyCode.Return))
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
        else
        {
            Debug.LogError("No GameObject is currently highlighted.");
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
            case MenuOption.Volume:
                return volumeSlider;
            default:
                return null;
        }
    }
}
