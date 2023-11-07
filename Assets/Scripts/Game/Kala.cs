using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class Kala : MonoBehaviour
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
        // Liikutetaan objektia eteenp‰in nopeuden verran
        transform.Translate(Vector3.forward * nopeus * Time.deltaTime);

        // Lasketaan et‰isyys kalan ja ter‰n v‰lill‰
        float etaisyys = Vector3.Distance(transform.position, tera.position);

        // Tarkistetaan, onko et‰isyys alle tuhoet‰isyyden ja tarkistetaan y-akseli
        if (etaisyys < tuhoetaisyys && Mathf.Abs(transform.position.y - tera.position.y) < 1.0f)
        {
            // Kala tuhoutuu
            Destroy(gameObject);

            // Lis‰tt‰v‰t pisteet
            int score = -15;

            // Lis‰t‰‰n pisteet listaan yhdess‰ pelaajan nimen kanssa
            scores.Add(new ScoreEntry { score = score, name = PlayerPrefs.GetString("Name") });

            // Tulostetaan pisteet konsoliin
            Debug.Log("Pisteet: " + score);
        }

        // Kala despawnaa, Jos se saavuttaa m‰‰ritetyn korkeuden
        if (transform.position.z <= despawnKorkeus)
        {
            // Kala tuhoutuu
            Destroy(gameObject);

            // Lis‰tt‰v‰t pisteet
            int score = 5;

            // Lis‰t‰‰n pisteet listaan yhdess‰ pelaajan nimen kanssa
            scores.Add(new ScoreEntry { score = score, name = PlayerPrefs.GetString("Name") });

            // Tulostetaan pisteet konsoliin
            Debug.Log("Pisteet: " + score);
        }
    }
}
