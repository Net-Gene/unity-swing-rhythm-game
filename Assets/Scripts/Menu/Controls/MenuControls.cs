using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuControls : MonoBehaviour
{
    // Luodaan lueteltu tyyppi (enum) MenuOption, joka m��rittelee valikkoasetukset
    public enum MenuOption { Play, Highscore, Options, Exit, Back, Graphics, Resolution, Volume, Fullscreen }

    // Suojattu virtuaalinen ominaisuus nykyiselle valikkoasetukselle
    protected virtual MenuOption CurrentOption { get; set; }

    // Viittaukset valikkopainikkeisiin
    public GameObject playButton;
    public GameObject highscoreButton;
    public GameObject optionsButton;
    public GameObject exitButton;
    public GameObject backButton;

    // Viittaukset valikkoruutuihin
    public GameObject mainMenu;
    public GameObject optionsMenu;

    // Viittaukset pudotusvalikon ja liukus��timen komponentteihin
    public TMP_Dropdown graphicsDropdown;
    public TMP_Dropdown resolutionDropdown;
    public GameObject volumeSlider;
    public GameObject fullscreenButton;

    // Korostettu painike ja tila, joka seuraa, onko painike korostettu
    private GameObject highlightedButton;
    private bool isButtonHighlighted = false;

    // Alustetaan nykyinen asetus "Play"
    protected virtual void Start()
    {
        CurrentOption = MenuOption.Play;
    }

    // P�ivitet��n joka ruutu
    protected virtual void Update()
    {
        // K�sitell��n p��valikon navigointia, jos p��valikko on aktiivinen
        if (SceneManager.GetSceneByName("Menu").isLoaded && mainMenu.activeSelf)
        {
            HandleMainMenuNavigation();
        }
        // K�sitell��n asetusvalikon navigointia, jos asetusvalikko on aktiivinen
        else if (SceneManager.GetSceneByName("Menu").isLoaded && optionsMenu.activeSelf)
        {
            HandleOptionsMenuNavigation();
        }
        // K�sitell��n HighScoreTable-ruutua
        else if (SceneManager.GetSceneByName("HighScoreTable").isLoaded)
        {
            if (!isButtonHighlighted)
            {
                HighlightButton(backButton);
                isButtonHighlighted = true;
            }

            // Simuloidaan painikkeen painallus "Enter"-n�pp�imell� tai kosketusn�yt�n napautuksella Androidilla
            if (Input.GetKeyDown(KeyCode.Return) || GameLogic.CheckTapOnAndroid())
            {
                SimulateButtonClick();
            }
        }
        else
        {
            // Poistetaan painikkeen korostus
            HighlightButton(null);
        }
    }

    // K�sitell��n p��valikon navigointia
    protected virtual void HandleMainMenuNavigation()
    {
        // K�sitell��n nuolin�pp�imien painalluksia tai pyyhk�isyt Androidilla
        if (Input.GetKeyDown(KeyCode.UpArrow) || GameLogic.CheckSwipeUpOnAndroid())
        {
            ChangeOption(MenuOption.Exit, MenuOption.Options, MenuOption.Highscore, MenuOption.Play);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) || GameLogic.CheckSwipeDownOnAndroid())
        {
            ChangeOption(MenuOption.Highscore, MenuOption.Options, MenuOption.Exit, MenuOption.Play);
        }
        // K�sitell��n "Enter"-n�pp�imen painallusta tai kosketusn�yt�n napautusta Androidilla
        else if (Input.GetKeyDown(KeyCode.Return) || GameLogic.CheckTapOnAndroid())
        {
            SimulateButtonClick();
        }
    }

    // K�sitell��n asetusvalikon navigointia
    protected virtual void HandleOptionsMenuNavigation()
    {
        // K�sitell��n nuolin�pp�imien painalluksia tai pyyhk�isyt Androidilla
        if (Input.GetKeyDown(KeyCode.UpArrow) || GameLogic.CheckSwipeUpOnAndroid())
        {
            ChangeOption(MenuOption.Volume, MenuOption.Resolution, MenuOption.Graphics, MenuOption.Fullscreen, MenuOption.Back);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) || GameLogic.CheckSwipeDownOnAndroid())
        {
            ChangeOption(MenuOption.Fullscreen, MenuOption.Graphics, MenuOption.Resolution, MenuOption.Volume, MenuOption.Back);
        }
        // K�sitell��n "Enter"-n�pp�imen painallusta tai kosketusn�yt�n napautusta Androidilla
        else if (Input.GetKeyDown(KeyCode.Return) || GameLogic.CheckTapOnAndroid())
        {
            SimulateButtonClick();
        }
    }

    // Vaihdetaan nykyist� asetusta
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

        // Korostetaan nykyinen painike
        HighlightButton(GetCurrentButton());
    }

    // Korostetaan painike
    protected virtual void HighlightButton(GameObject button)
    {
        Debug.Log("Korostetaan: " + (button != null ? button.name : "null"));

        // Asetetaan valittu peliobjekti korostetuksi
        UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject(button);
    }

    // Simuloidaan painikkeen painallus
    protected virtual void SimulateButtonClick()
    {
        Button buttonComponent = highlightedButton?.GetComponent<Button>();
        if (buttonComponent != null)
        {
            // Kutsutaan painikkeen onClick-tapahtumaa
            buttonComponent.onClick.Invoke();
        }
        else
        {
            Debug.LogError("Valitulla peliobjektilla ei ole Button-komponenttia.");
        }
    }

    // Palautetaan nykyinen painike nykyisen asetuksen perusteella
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
