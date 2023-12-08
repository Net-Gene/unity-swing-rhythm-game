using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameLogic : MonoBehaviour
{
    public static int score;
    public TMP_Text gameScoreText;
 

    private int currentScore;  // Nykyinen pistemäärä

    // Tarkista pyyhkäisy vasemmalle Androidilla
    public static bool CheckSwipeLeftOnAndroid()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            return touch.phase == TouchPhase.Moved && touch.deltaPosition.x < 0;
        }
        return false;
    }

    // Tarkista pyyhkäisy oikealle Androidilla
    public static bool CheckSwipeRightOnAndroid()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            return touch.phase == TouchPhase.Moved && touch.deltaPosition.x > 0;
        }
        return false;
    }

    // Tarkista pyyhkäisy ylöspäin Androidilla
    public static bool CheckSwipeUpOnAndroid()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            return touch.phase == TouchPhase.Moved && touch.deltaPosition.y > 0;
        }
        return false;
    }

    // Tarkista pyyhkäisy alaspäin Androidilla
    public static bool CheckSwipeDownOnAndroid()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            return touch.phase == TouchPhase.Moved && touch.deltaPosition.y < 0;
        }
        return false;
    }

    // Tarkista näytön kosketus Androidilla
    public static bool CheckTapOnAndroid()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            return touch.phase == TouchPhase.Began;
        }
        return false;
    }

    private void Start()
    {
        // Check if the scene named "game" is active and enabled
        Scene gameScene = SceneManager.GetSceneByName("Game");
        if (gameScene.isLoaded)
        {
            GameLogic.score = 0;
        }

        currentScore = GameLogic.score;  // Alustetaan nykyinen pistemäärä pelin alussa
    }

    private void Update()
    {
        // Käsittele Pause-valikon navigointia
        if (SceneManager.GetSceneByName("Game").isLoaded)
        {
            gameScoreText.text = GameLogic.score.ToString();
        }
        // Päivitä pistemääräteksti
    }
}
