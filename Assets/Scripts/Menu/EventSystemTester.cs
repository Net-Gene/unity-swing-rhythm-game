using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MenuSelection : MonoBehaviour
{
    [SerializeField] public GameObject mainMenu;
    [SerializeField] public GameObject optionMenu;
    [SerializeField] public GameObject highscoreMenu;
    [SerializeField] public Button mainMenuSelectButton;
    [SerializeField] public Button highscoreMenuSelectButton;
    [SerializeField] public Button optionMenuSelectButton;

    private void Start()
    {
        // Ensure no menu is initially selected
        EventSystem.current.SetSelectedGameObject(null);
    }

    private void Update()
    {
        // When no menu is active, set focus back to the last selected button
        if (!mainMenu.activeSelf && !optionMenu.activeSelf && !highscoreMenu.activeSelf)
        {
            EventSystem.current.SetSelectedGameObject(null);
        }
    }

    public void MainMenuOpen()
    {
        if (mainMenu.activeSelf == true)
        {
            EventSystem.current.SetSelectedGameObject(mainMenuSelectButton.gameObject);
        }
    }

    public void OptionMenuOpen()
    {
        if (optionMenu.activeSelf == true)
        {
            EventSystem.current.SetSelectedGameObject(optionMenuSelectButton.gameObject);
        }
    }

    public void HighscoreMenuOpen()
    {
        if (highscoreMenu.activeSelf == true)
        {
            EventSystem.current.SetSelectedGameObject(highscoreMenuSelectButton.gameObject);
        }
    }
}
