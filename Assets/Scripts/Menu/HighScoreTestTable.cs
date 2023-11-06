using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HighScoreTestTable : MonoBehaviour
{
    public void MainMenu()
    {
        // Ladataan MainMenu scenario
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 3);
    }
}
