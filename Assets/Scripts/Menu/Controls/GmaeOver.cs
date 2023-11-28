using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static GameOverMenuControls;

public class GmaeOver : MonoBehaviour
{
    public enum GameOverMenuOption { Quit, Retry, MainMenu }

    protected virtual GameOverMenuOption CurrentOption { get; set; }
    // MenuOption

    public GameObject quitButton;
    public GameObject retryButton;
    public GameObject mainMenuButton;

    private GameObject highlightedButton;

    /*private bool playButtonHighlighted = false;
    private bool isbackButtonLoaded = false;*/
    private bool isGameOverSceneLoaded = false;
    private bool backButtonHighlighted = false;



    protected virtual void SetCurrentOption(GameOverMenuOption option)
    {
        CurrentOption = option;
    }

    private GameObject retryScreen;

    protected virtual void Start()
    {
        CurrentOption = GameOverMenuOption.Retry;
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
        if (SceneManager.GetSceneByName("GameOver").isLoaded && retryScreen != null && retryScreen.activeSelf)
        {
            Debug.Log("Entered gameoverscene");
            GameOverMenuControls gameOverMenu = FindObjectOfType<GameOverMenuControls>();
            if (gameOverMenu != null)
            {
                HandleMainMenuNavigation();
                HighlightButton(gameOverMenu.quitButton);
            }
        }

    }

    protected virtual void HandleMainMenuNavigation()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            ChangeOption(GameOverMenuOption.Quit, GameOverMenuOption.Retry, GameOverMenuOption.MainMenu);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            ChangeOption(GameOverMenuOption.Retry, GameOverMenuOption.MainMenu, GameOverMenuOption.Quit);

        }
        else if (Input.GetKeyDown(KeyCode.Return))
        {
            SimulateButtonClick();
        }
    }
    

    protected virtual void ChangeOption(params GameOverMenuOption[] options)
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
            case GameOverMenuOption.Quit:
                return quitButton;
            case GameOverMenuOption.Retry:
                return retryButton;
            case GameOverMenuOption.MainMenu:
                return mainMenuButton;
            default:
                return null;
        }
    }
}
