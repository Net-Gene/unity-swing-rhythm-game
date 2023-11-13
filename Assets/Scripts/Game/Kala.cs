using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class Kala : MonoBehaviour
{
    // Alkuperäinen tukin ilmestymisväli minimi ja maksimi
    public float initialSpawnIntervalMin = 6f;
    public float initialSpawnIntervalMax = 5f;

    // Korkeus, jossa tukki poistetaan
    public float despawnKorkeus = -10f;

    // Tukin liikkumisnopeus
    private float nopeus = 5f;

    // Tera-peliobjekti ja tuhoetäisyys
    public Transform tera;

    public float tuhoetaisyys = 2.0f;

    // Muuttuja, joka tallentaa terän nimen
    public string teranNimi = "Tera";

    // Lista pisteille
    private List<ScoreEntry> scores = new List<ScoreEntry>();

    // Viittaus HighScoreTable-skriptiin
    public HighScoreTable highScoreTable;

    // Luokka pisteiden ja pelaajan nimen tallentamiseksi
    [System.Serializable]
    public class ScoreEntry
    {
        public int score;    // Pisteet
        public string name;  // Nimi
    }

    private void Start()
    {
        // Etsitään HighScoreTable-skripti
        highScoreTable = GameObject.FindObjectOfType<HighScoreTable>();
    }

    private void Update()
    {
        // Liikutetaan objektia eteenpäin nopeuden verran
        transform.Translate(Vector3.forward * nopeus * Time.deltaTime);

        // Etsitään terä-objekti dynaamisesti sen nimen perusteella
        if (tera == null)
        {
            GameObject teraObj = GameObject.Find(teranNimi);

            if (teraObj != null)
            {
                tera = teraObj.transform;
            }
            else
            {
                Debug.LogError("Tera-objektia ei löytynyt. Tarkista nimi ja varmista, että se on aktiivinen.");
                return;
            }
        }

        // Lasketaan etäisyys kalan ja terän välillä
        float etaisyys = Vector3.Distance(transform.position, tera.position);

        // Tarkistetaan, onko etäisyys alle tuhoetäisyyden ja tarkistetaan y-akseli
        if (etaisyys < tuhoetaisyys && Mathf.Abs(transform.position.y - tera.position.y) < 1.0f)
        {
            // Kala tuhoutuu
            Destroy(gameObject);

            // Lisättävät pisteet
            int score = -15;

            // Lisätään pisteet listaan yhdessä pelaajan nimen kanssa
            scores.Add(new ScoreEntry { score = score, name = PlayerPrefs.GetString("Name") });

            // Tulostetaan pisteet konsoliin
            Debug.Log("Pisteet: " + score);
        }

        // Kala despawnaa, Jos se saavuttaa määritetyn korkeuden
        if (transform.position.z <= despawnKorkeus)
        {
            // Kala tuhoutuu
            Destroy(gameObject);

            // Lisättävät pisteet
            int score = 5;

            // Lisätään pisteet listaan yhdessä pelaajan nimen kanssa
            scores.Add(new ScoreEntry { score = score, name = PlayerPrefs.GetString("Name") });

            // Tulostetaan pisteet konsoliin
            Debug.Log("Pisteet: " + score);
        }
    }
}