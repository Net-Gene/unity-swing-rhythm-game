using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverMenuControls : MenuControls
{
    public enum GameOverMenuOption { Quit, Retry, MainMenu }


    public GameObject quitButton;
    public GameObject retryButton;
    public GameObject mainMenuButton;

    private bool quitButtonHighlighted = false;

    protected override void ChangeOption(params MenuOption[] options)
    {
        base.ChangeOption(options);

        // Additional logic specific to GameOverMenuControls
        // ...
    }

    protected override void HandleMainMenuNavigation()
    {
        base.HandleMainMenuNavigation();
        /*
        if (!quitButtonHighlighted)
        {
            HighlightButton(quitButton);
            quitButtonHighlighted = true;
        }
        */
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            ChangeOption((MenuOption)GameOverMenuOption.Quit, (MenuOption)GameOverMenuOption.MainMenu, (MenuOption)GameOverMenuOption.Retry);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            ChangeOption((MenuOption)GameOverMenuOption.Retry, (MenuOption)GameOverMenuOption.MainMenu, (MenuOption)GameOverMenuOption.Quit);
        }
        else if (Input.GetKeyDown(KeyCode.Return))
        {
            SimulateButtonClick();
        }
    }

    protected override GameObject GetCurrentButton()
    {
        switch ((GameOverMenuOption)CurrentOption)
        {
            case GameOverMenuOption.Quit:
                return quitButton;
            case GameOverMenuOption.Retry:
                return retryButton;
            case GameOverMenuOption.MainMenu:
                return mainMenuButton;
            default:
                return base.GetCurrentButton();
        }
    }
}
