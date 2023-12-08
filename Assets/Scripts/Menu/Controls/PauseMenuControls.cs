using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenuControls : MonoBehaviour
{
    public enum PauseMenuOption { Resume, Menu, Quit }

    // Nykyinen valinta
    protected virtual PauseMenuOption CurrentOption { get; set; }

    // Napit valinnoille
    public GameObject resumeButton;
    public GameObject menuButton;
    public GameObject quitButton;

    private GameObject highlightedButton;

    // Pause-valikko
    public GameObject pauseMenu;

    private bool isButtonHighlighted = false;

    protected virtual void SetCurrentOption(PauseMenuOption option)
    {
        CurrentOption = option;
    }

    protected virtual void Start()
    {
        CurrentOption = PauseMenuOption.Resume;
    }

    protected virtual void Update()
    {
        // Käsittele Pause-valikon navigointia
        if (pauseMenu.activeSelf)
        {
            HandlePauseMenuMenuNavigation();
        }
    }

    protected virtual void HandlePauseMenuMenuNavigation()
    {
        // Korosta nappulaa, jos sitä ei ole vielä korostettu
        if (!isButtonHighlighted)
        {
            HighlightButton(resumeButton);
            isButtonHighlighted = true;
        }

        // Vaihda valintaa ylös- ja alasnuolinäppäimillä tai kosketuksella Androidilla
        if (Input.GetKeyDown(KeyCode.UpArrow) || GameLogic.CheckSwipeUpOnAndroid())
        {
            ChangeOption(PauseMenuOption.Quit, PauseMenuOption.Menu, PauseMenuOption.Resume);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) || GameLogic.CheckSwipeDownOnAndroid())
        {
            ChangeOption(PauseMenuOption.Menu, PauseMenuOption.Quit, PauseMenuOption.Resume);
        }
        // Simuloi napin painallus Enter-näppäimellä tai kosketuksella Androidilla
        else if (Input.GetKeyDown(KeyCode.Return) || GameLogic.CheckTapOnAndroid())
        {
            isButtonHighlighted = false;
            SimulateButtonClick();
        }
    }

    protected virtual void ChangeOption(params PauseMenuOption[] options)
    {
        // Vaihda valintaa ympyränmuotoisesti valittujen vaihtoehtojen välillä
        int index = Array.IndexOf(options, CurrentOption);
        if (index == -1)
        {
            CurrentOption = options[0];
        }
        else
        {
            CurrentOption = options[(index + 1) % options.Length];
        }

        // Korosta valittu nappula
        HighlightButton(GetCurrentButton());
    }

    protected virtual void HighlightButton(GameObject button)
    {
        Debug.Log("Korostetaan: " + (button != null ? button.name : "null"));

        // Simuloi hiiren kohdistusta valittuun nappulaan
        UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject(button);
    }

    protected virtual void SimulateButtonClick()
    {
        // Hae nappulakomponentti ja kutsu onClick-metodia
        Button buttonComponent = highlightedButton?.GetComponent<Button>();
        if (buttonComponent != null)
        {
            buttonComponent.onClick.Invoke();
        }
        else
        {
            Debug.LogError("Valitulla peliobjektilla ei ole Button-komponenttia.");
        }
    }

    protected virtual GameObject GetCurrentButton()
    {
        // Palauta nykyinen valintaa vastaava nappula
        switch (CurrentOption)
        {
            case PauseMenuOption.Quit:
                return quitButton;
            case PauseMenuOption.Menu:
                return menuButton;
            case PauseMenuOption.Resume:
                return resumeButton;
            default:
                return null;
        }
    }
}


