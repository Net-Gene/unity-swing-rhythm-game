using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class HighScoreTable : MonoBehaviour
{
    public GameObject entryContainer;   // GameObject, joka toimii pistetaulun sisällön säiliönä
    public GameObject entryTemplate;    // GameObject-malli yksittäiselle pisteennäyttöelementille
    public TMPro.TMP_Text scoreText;    // Tekstikenttä korkeimman pistemäärän näyttämiseen
    public TMPro.TMP_Text nameText;     // Tekstikenttä pelaajan nimen näyttämiseen

    // Määritellään luokka, joka pitää sisällään pistemäärän ja pelaajan nimen
    [System.Serializable]
    public class ScoreEntry
    {
        public int score;   // Pistemäärä
        public string name; // Pelaajan nimi
    }

    public List<ScoreEntry> scores = new List<ScoreEntry>(); // Lista, joka pitää sisällään pistemäärät ja pelaajien nimet

    private void Start()
    {
        // Lisätään nykyinen pistemäärä ja pelaajan nimi listaan
        int currentScore = PlayerPrefs.GetInt("CurrentScore", 0); // Haetaan nykyinen pistemäärä pelaajan tilapäismuistista
        string playerName = PlayerPrefs.GetString("Name"); // Haetaan pelaajan nimi pelaajan tilapäismuistista
        scores.Add(new ScoreEntry { score = currentScore, name = playerName });

        // Järjestetään pistemäärät laskevaan järjestykseen
        scores.Sort((a, b) => b.score.CompareTo(a.score));

        // Alustetaan pistetaulukko
        AlustaPisteTaulukko();

        // Näytetään korkein pistemäärä
        int score = scores.Count > 0 ? scores[0].score : 0;
        scoreText.text = score.ToString();

        // Tallennetaan päivitetty pistetaulukko
        SaveHighScores();
    }

    private void AlustaPisteTaulukko()
    {
        entryTemplate.SetActive(false); // Piilotetaan mallipohja, koska sitä ei tarvita näytöllä

        float pohjapohjanKorkeus = 20f; // Yksittäisen pistenäytön korkeus

        for (int i = 0; i < scores.Count; i++)
        {
            GameObject entryTransform = Instantiate(entryTemplate, entryContainer.transform); // Luodaan uusi pistenäyttö kopioimalla mallipohja
            RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
            entryRectTransform.anchoredPosition = new Vector2(0, -pohjapohjanKorkeus * i); // Asetetaan pistenäytön sijainti sen korkeuden mukaan
            entryTransform.SetActive(true); // Näytetään pistenäyttö

            string sijoitusTeksti = HaeSijoitusTeksti(i + 1); // Haetaan sijoituksen tekstiesitys
            entryTransform.transform.Find("posText").GetComponent<TMPro.TMP_Text>().text = sijoitusTeksti; // Asetetaan sijoituksen teksti pistenäyttöön

            entryTransform.transform.Find("scoreText").GetComponent<TMPro.TMP_Text>().text = scores[i].score.ToString(); // Asetetaan pistemäärä pistenäyttöön
            entryTransform.transform.Find("nameText").GetComponent<TMPro.TMP_Text>().text = scores[i].name; // Asetetaan pelaajan nimi pistenäyttöön
        }
    }

    private string HaeSijoitusTeksti(int sijoitus)
    {
        if (sijoitus == 1) return "1ST";
        if (sijoitus == 2) return "2ND";
        if (sijoitus == 3) return "3RD";
        return sijoitus + "TH";
    }

    // Tallentaa päivitetyn pistetaulukon pelaajan tilapäismuistiin
    public void SaveHighScores()
    {
        for (int i = 0; i < scores.Count; i++)
        {
            PlayerPrefs.SetInt("HighScore" + i, scores[i].score); // Tallentaa pistemäärän pelaajan tilapäismuistiin
            PlayerPrefs.SetString("Name" + i, scores[i].name); // Tallentaa pelaajan nimen pelaajan tilapäismuistiin
        }
    }

    // Lisää uuden pistemäärän ja pelaajan nimen pistetaulukkoon
    public void AddNewScore(string playerName, int score)
    {
        // Luo uusi pistemäärämerkintä
        ScoreEntry newEntry = new ScoreEntry { name = playerName, score = score };

        // Lisää uusi merkintä pistetaulukkoon
        scores.Add(newEntry);

        // Järjestä pistemäärät pitääksesi järjestyksen
        scores.Sort((a, b) => b.score.CompareTo(a.score));

        // Valinnaisesti voit päivittää käyttöliittymää pistetaulukon muutosten mukaisesti
        AlustaPisteTaulukko();

        // Tallenna pistetaulukko
        SaveHighScores();
    }
}
