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
    protected override void ChangeOption(params MenuOption[] options)
    {
        base.ChangeOption(options);

        // Additional logic specific to GameOverMenuControls
        // ...
    }

    protected override void MoveSelectionUp()
        => ChangeOption((MenuOption)GameOverMenuOption.Quit, (MenuOption)GameOverMenuOption.MainMenu, (MenuOption)GameOverMenuOption.Retry);

    protected override void MoveSelectionDown()
    {
        switch ((GameOverMenuOption)CurrentOption)
        {
            case GameOverMenuOption.Quit:
                CurrentOption = (MenuOption)GameOverMenuOption.Retry;
                break;
            case GameOverMenuOption.Retry:
                CurrentOption = (MenuOption)GameOverMenuOption.MainMenu;
                break;
            case GameOverMenuOption.MainMenu:
                CurrentOption = (MenuOption)GameOverMenuOption.Quit;
                break;
            // Add additional cases as needed
            default:
                base.MoveSelectionDown();
                return;
        }

        HighlightButton(GetCurrentButton());
    }

    protected override void Start()
    {
        CurrentOption = (MenuOption)GameOverMenuOption.Quit;
        HighlightButtonBasedOnScene();
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
