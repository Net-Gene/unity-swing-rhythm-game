using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MenuSelection : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject optionMenu;
    public GameObject highscoreMenu;
    public Button mainMenuSelectButton;
    public Button highscoreMenuSelectButton;
    public Button optionMenuSelectButton;

    private EventSystem eventSystem;
    private GameObject lastSelectedButton;

    private void Start()
    {
        eventSystem = EventSystem.current; // Get the current EventSystem

        if (eventSystem == null)
        {
            Debug.LogError("No EventSystem found. You should add one to your scene.");
        }
    }

    private void Update()
    {
        if (mainMenu.activeSelf)
        {
            if (eventSystem.currentSelectedGameObject != mainMenuSelectButton.gameObject)
            {
                // If Main Menu is active and the selected button is not the current selected, set the "first selected" value in the EventSystem
                eventSystem.firstSelectedGameObject = mainMenuSelectButton.gameObject;
                lastSelectedButton = mainMenuSelectButton.gameObject;
            }
        }
        else if (optionMenu.activeSelf)
        {
            if (eventSystem.currentSelectedGameObject != optionMenuSelectButton.gameObject)
            {
                // If Option Menu is active and the selected button is not the current selected, set the "first selected" value in the EventSystem
                eventSystem.firstSelectedGameObject = optionMenuSelectButton.gameObject;
                lastSelectedButton = optionMenuSelectButton.gameObject;
            }
        }
        else if (highscoreMenu.activeSelf)
        {
            if (eventSystem.currentSelectedGameObject != highscoreMenuSelectButton.gameObject)
            {
                // If Highscore Menu is active and the selected button is not the current selected, set the "first selected" value in the EventSystem
                eventSystem.firstSelectedGameObject = highscoreMenuSelectButton.gameObject;
                lastSelectedButton = highscoreMenuSelectButton.gameObject;
            }
        }

        if (!mainMenu.activeSelf && !optionMenu.activeSelf && !highscoreMenu.activeSelf)
        {
            // When no menu is active, set focus back to the last selected button
            eventSystem.SetSelectedGameObject(lastSelectedButton);
        }
    }
}
