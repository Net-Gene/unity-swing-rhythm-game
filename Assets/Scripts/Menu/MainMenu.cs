using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour
{
    public TMP_InputField passwordInputField;

    public TMP_Text passwordTextComponent;

    public GameObject highScoreTable;

    public GameObject passwordObject;

    // Metodi pelin pelaamiseen
    public void PlayGame()
    {
        // Lataa seuraava peliskenaario (+1 nykyisestä skenaarionumerosta)
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void HighScoreScene()
    {
        // Lataa seuraava peliskenaario (+1 nykyisestä skenaarionumerosta)
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 3);
    }

    public void GoToMainMenu()
    {
        // Lataa seuraava peliskenaario (+1 nykyisestä skenaarionumerosta)
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 3);
    }

    // Metodi pelin sulkemiseen
    public void QuitGame()
    {
        // Lopettaa sovelluksen suorituksen
        Application.Quit();
    }

    // New method to delete high score data
    public void DeleteHighScoreData()
    {
        string correctPassword = "Pilke"; // Replace with your actual correct password
        string filePath = Application.persistentDataPath + "/hiScoreData.dat";

        // Trim the input to remove leading and trailing whitespaces
        string enteredPassword = passwordInputField.text.Trim();

        if (enteredPassword == correctPassword)
        {
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
                Debug.Log("High Score Data deleted.");

                // Assuming you have references to the game objects
                highScoreTable.SetActive(true);

                passwordObject.SetActive(false); // SetActive(false) to hide it
            }
            else
            {
                Debug.Log("No High Score Data to delete.");
            }
        }
        else
        {
            Debug.Log("Wrong password");
            passwordTextComponent.color = Color.red;
        }
    }
}
