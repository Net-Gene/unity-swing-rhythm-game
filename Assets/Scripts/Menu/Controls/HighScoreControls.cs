using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScoreControls : MonoBehaviour
{
    // Vaihtoehdot pelin loppumismenulle
    public enum HighScoreMenuOption { Reset, Back}

    // T‰m‰ property mahdollistaa nykyisen valinnan seuraamisen ja muuttamisen
    protected virtual HighScoreMenuOption CurrentOption { get; set; }

    // Pelin loppumismenun vaihtoehdot
    public GameObject backButton;       // Lopeta-nappi
    public GameObject resetButton;      // Yrit‰ uudelleen -nappi
    
    private GameObject highlightedButton;  // Valittu nappi

    //private bool isButtonHighlighted = false;  // Onko nappi korostettu

    // Alustetaan nykyinen vaihtoehto Yrit‰ uudelleen -napille
    protected virtual void Start()
    {
        CurrentOption = HighScoreMenuOption.Back;
    }

    // P‰ivitet‰‰n jatkuvasti loppumismenun n‰yttˆ‰
    protected virtual void Update()
    {
        HandleHighScoreMenuNavigation();
    }

    // K‰sitell‰‰n navigointi Yrit‰ uudelleen -valikossa
    protected virtual void HandleHighScoreMenuNavigation()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            ChangeOption(HighScoreMenuOption.Reset, HighScoreMenuOption.Back);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            ChangeOption(HighScoreMenuOption.Reset, HighScoreMenuOption.Back);
        }
        else if (Input.GetKeyDown(KeyCode.Return))
        {
            SimulateButtonClick();
        }
    }

    // Vaihdetaan nykyist‰ vaihtoehtoa
    protected virtual void ChangeOption(params HighScoreMenuOption[] options)
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

    // Korostetaan nappi (simuloidaan hiiren liikett‰)
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
            case HighScoreMenuOption.Reset:
                return resetButton;
            case HighScoreMenuOption.Back:
                return backButton;
            default:
                return null;
        }
    }
}
