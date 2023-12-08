using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Windows;

/// <summary>
/// HiScoreInput
/// </summary>
public class HiScoreInput : MonoBehaviour
{
    /// <summary>
    /// List of Letters
    /// </summary>
    [SerializeField]List<GameObject> listOfLetters = new List<GameObject>();

    /// <summary>
    /// Score Text
    /// </summary>
    [SerializeField] public Text scoreText;


    /// <summary>
    /// Selected Letter
    /// </summary>
    private int selectedLetter = 0;

    /// <summary>
    /// Number of Letters
    /// </summary>
    //private int numberOfLetters = 3;

    /// <summary>
    /// Default Color
    /// </summary>
    private Color defaultColor;

    [SerializeField] public GameObject gameObjectToActivate;
    [SerializeField] public GameObject gameObjectToDeactivate;

    /// <summary>
    /// This function is called when the object becomes enabled and active.
    /// </summary>
    void OnEnable()
    {
        selectedLetter = 0;
        listOfLetters[selectedLetter].GetComponent<Animator>().enabled = true;
        scoreText.text = GameLogic.score.ToString();


    }

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Start()
    {
        defaultColor = listOfLetters[selectedLetter].GetComponent<Text>().color;
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {

        if (UnityEngine.Input.GetKeyDown(KeyCode.LeftArrow) || GameLogic.CheckSwipeLeftOnAndroid())
            PrevLetter();

        if (UnityEngine.Input.GetKeyDown(KeyCode.RightArrow) || GameLogic.CheckSwipeRightOnAndroid())
            NextLetter();

        if (UnityEngine.Input.GetKeyDown(KeyCode.UpArrow) || GameLogic.CheckSwipeUpOnAndroid())
            NextAlphaBet();

        if (UnityEngine.Input.GetKeyDown(KeyCode.DownArrow) || GameLogic.CheckSwipeDownOnAndroid())
            PrevAlphaBet();

        if (UnityEngine.Input.GetKeyDown(KeyCode.Return) || GameLogic.CheckTapOnAndroid())
        {
            HiScoreInputController.Instance.Save(listOfLetters[0].GetComponent<Text>().text
                + listOfLetters[1].GetComponent<Text>().text +
                listOfLetters[2].GetComponent<Text>().text, GameLogic.score);

            // Activate and deactivate GameObjects
            gameObjectToActivate.SetActive(true);
            gameObjectToDeactivate.SetActive(false);
        }
    }

    /// <summary>
    /// NextAlphaBet
    /// </summary>
    void NextAlphaBet() {
        // 13. Valitulle kirjaimelle annetaan arvoksi seuraava kirjain aakkosista
        // Vinkki! Käytä char muuttujaa hyväksi ja ASCII koodeja, kirjaimina käytetään A-Z isot kirjaimet
        char letter = listOfLetters[selectedLetter].GetComponent<Text>().text.ToString().ToCharArray()[0];

        
        letter++;
        if (letter > 'Z')
        {
            letter = 'A';
        }
        listOfLetters[selectedLetter].GetComponent<Text>().text = letter.ToString();
    }


    /// <summary>
    /// PrevAlphaBet
    /// </summary>
    void PrevAlphaBet() {

        char letter = listOfLetters[selectedLetter].GetComponent<Text>().text.ToString().ToCharArray()[0];

        letter--;
        if (letter < 'A')
        {
            letter = 'Z';
        }
        listOfLetters[selectedLetter].GetComponent<Text>().text = letter.ToString();
    }


    /// <summary>
    /// NextLetter
    /// </summary>
    void NextLetter() {
        listOfLetters[selectedLetter].GetComponent<Animator>().enabled = false;
        listOfLetters[selectedLetter].GetComponent<Text>().color = defaultColor;
        selectedLetter++;

        if (selectedLetter > 2)
        {
            selectedLetter = 0;
        }

        listOfLetters[selectedLetter].GetComponent<Animator>().enabled = true;
    }

    /// <summary>
    /// PrevLetter
    /// </summary>
    void PrevLetter() {
        listOfLetters[selectedLetter].GetComponent<Animator>().enabled = false;
        listOfLetters[selectedLetter].GetComponent<Text>().color = defaultColor;
        selectedLetter--;

        if (selectedLetter < 0)
        {
            selectedLetter = 2;
        }

        listOfLetters[selectedLetter].GetComponent<Animator>().enabled = true;
    }
}
