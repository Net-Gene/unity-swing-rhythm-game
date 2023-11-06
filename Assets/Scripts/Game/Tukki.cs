using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class Tukki : MonoBehaviour
{
    // Alkuper‰inen tukin ilmestymisv‰li minimi ja maksimi
    public float initialSpawnIntervalMin = 6f;
    public float initialSpawnIntervalMax = 5f;

    // Korkeus, jossa tukki poistetaan
    public float despawnKorkeus = -10f;

    // Pelaajan ja kirveen peliobjektit
    public GameObject pelaaja;
    public GameObject kirves;

    // Tukin liikkumisnopeus
    private float nopeus = 5f;

    // Tera-peliobjekti ja tuhoet‰isyys
    public Transform tera;
    public float tuhoetaisyys = 1.0f;

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
        // Etsit‰‰n pelaajan ja kirveen peliobjektit
        pelaaja = GameObject.Find("Pelaaja");
        kirves = GameObject.Find("Kirves");

        // Etsit‰‰n HighScoreTable-skripti
        highScoreTable = GameObject.FindObjectOfType<HighScoreTable>();
    }

    private void Update()
    {
        // Liikutetaan objektia taaksep‰in nopeuden verran
        transform.Translate(Vector3.back * nopeus * Time.deltaTime);

        // Lasketaan et‰isyys tukin ja ter‰n v‰lill‰
        float etaisyys = Vector3.Distance(transform.position, tera.position);

        // Tarkistetaan, onko et‰isyys alle tuhoet‰isyyden ja tarkistetaan y-akseli
        if (etaisyys < tuhoetaisyys && Mathf.Abs(transform.position.y - tera.position.y) < 1.0f)
        {
            // Tukki tuhoutuu
            Destroy(gameObject);

            // Lasketaan pisteet pelilogiikan perusteella
            int score = 10;

            // Lis‰t‰‰n pisteet listaan yhdess‰ pelaajan nimen kanssa
            scores.Add(new ScoreEntry { score = score, name = PlayerPrefs.GetString("Name") });

            // Tulostetaan pisteet konsoliin
            Debug.Log("Pisteet: " + score);
        }

        // Jos tukki saavuttaa m‰‰ritetyn korkeuden, peli p‰‰ttyy
        if (transform.position.z <= despawnKorkeus)
        {
            // Tukki tuhoutuu
            Destroy(gameObject);

            // Tulostetaan peli p‰‰ttyneeksi
            Debug.Log("Game Over");

            // Siirryt‰‰n seuraavaan pelisceneen
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
