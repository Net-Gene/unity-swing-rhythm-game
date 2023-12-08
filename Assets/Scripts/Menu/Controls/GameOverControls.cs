using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static GameOverControls;

public class GameOverControls : MonoBehaviour
{
    // Vaihtoehdot pelin loppumismenulle
    public enum GameOverMenuOption { Quit, Retry, MainMenu }

    // T�m� property mahdollistaa nykyisen valinnan seuraamisen ja muuttamisen
    protected virtual GameOverMenuOption CurrentOption { get; set; }

    // Pelin loppumismenun vaihtoehdot
    public GameObject quitButton;       // Lopeta-nappi
    public GameObject retryButton;      // Yrit� uudelleen -nappi
    public GameObject mainMenuButton;   // P��valikko-nappi

    private GameObject highlightedButton;  // Valittu nappi

    public GameObject retryScreen;      // Yrit� uudelleen -n�ytt�

    private bool isButtonHighlighted = false;  // Onko nappi korostettu

    // Alustetaan nykyinen vaihtoehto Yrit� uudelleen -napille
    protected virtual void Start()
    {
        CurrentOption = GameOverMenuOption.Retry;
    }

    // P�ivitet��n jatkuvasti loppumismenun n�ytt��
    protected virtual void Update()
    {
        if (retryScreen.activeSelf)
        {
            HandleRetryMenuNavigation();
        }
    }

    // K�sitell��n navigointi Yrit� uudelleen -valikossa
    protected virtual void HandleRetryMenuNavigation()
    {
        if (!isButtonHighlighted)
        {
            HighlightButton(retryButton);
            isButtonHighlighted = true;
        }

        if (Input.GetKeyDown(KeyCode.UpArrow) || GameLogic.CheckSwipeUpOnAndroid())
        {
            ChangeOption(GameOverMenuOption.Quit, GameOverMenuOption.MainMenu, GameOverMenuOption.Retry);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) || GameLogic.CheckSwipeDownOnAndroid())
        {
            ChangeOption(GameOverMenuOption.MainMenu, GameOverMenuOption.Quit, GameOverMenuOption.Retry);
        }
        else if (Input.GetKeyDown(KeyCode.Return) || GameLogic.CheckTapOnAndroid())
        {
            isButtonHighlighted = false;
            SimulateButtonClick();
        }
    }

    


    // Vaihdetaan nykyist� vaihtoehtoa
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

    // Korostetaan nappi (simuloidaan hiiren liikett�)
    protected virtual void HighlightButton(GameObject button)
    {
        Debug.Log("Korostetaan: " + (button != null ? button.name : "null"));
        highlightedButton = button;

        // Simuloidaan hiiren valintaa valitulla napilla
        UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject(button);
    }

    // Simuloidaan nappulan klikkausta
    protected virtual void SimulateButtonClick()
    {
        Button buttonComponent = highlightedButton?.GetComponent<Button>();
        if (buttonComponent != null)
        {
            buttonComponent.onClick.Invoke();
        }
        else
        {
            Debug.LogError("Valitulla GameObjectilla ei ole Button-komponenttia.");
        }
    }

    // Palautetaan nykyinen valittu nappi
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
