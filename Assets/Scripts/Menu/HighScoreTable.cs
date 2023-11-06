using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class HighScoreTable : MonoBehaviour
{
    public GameObject entryContainer;   // GameObject, joka toimii pistetaulun sis�ll�n s�ili�n�
    public GameObject entryTemplate;    // GameObject-malli yksitt�iselle pisteenn�ytt�elementille
    public TMPro.TMP_Text scoreText;    // Tekstikentt� korkeimman pistem��r�n n�ytt�miseen
    public TMPro.TMP_Text nameText;     // Tekstikentt� pelaajan nimen n�ytt�miseen

    // M��ritell��n luokka, joka pit�� sis�ll��n pistem��r�n ja pelaajan nimen
    [System.Serializable]
    public class ScoreEntry
    {
        public int score;   // Pistem��r�
        public string name; // Pelaajan nimi
    }

    public List<ScoreEntry> scores = new List<ScoreEntry>(); // Lista, joka pit�� sis�ll��n pistem��r�t ja pelaajien nimet

    private void Start()
    {
        // Lis�t��n nykyinen pistem��r� ja pelaajan nimi listaan
        int currentScore = PlayerPrefs.GetInt("CurrentScore", 0); // Haetaan nykyinen pistem��r� pelaajan tilap�ismuistista
        string playerName = PlayerPrefs.GetString("Name"); // Haetaan pelaajan nimi pelaajan tilap�ismuistista
        scores.Add(new ScoreEntry { score = currentScore, name = playerName });

        // J�rjestet��n pistem��r�t laskevaan j�rjestykseen
        scores.Sort((a, b) => b.score.CompareTo(a.score));

        // Alustetaan pistetaulukko
        AlustaPisteTaulukko();

        // N�ytet��n korkein pistem��r�
        int score = scores.Count > 0 ? scores[0].score : 0;
        scoreText.text = score.ToString();

        // Tallennetaan p�ivitetty pistetaulukko
        SaveHighScores();
    }

    private void AlustaPisteTaulukko()
    {
        entryTemplate.SetActive(false); // Piilotetaan mallipohja, koska sit� ei tarvita n�yt�ll�

        float pohjapohjanKorkeus = 20f; // Yksitt�isen pisten�yt�n korkeus

        for (int i = 0; i < scores.Count; i++)
        {
            GameObject entryTransform = Instantiate(entryTemplate, entryContainer.transform); // Luodaan uusi pisten�ytt� kopioimalla mallipohja
            RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
            entryRectTransform.anchoredPosition = new Vector2(0, -pohjapohjanKorkeus * i); // Asetetaan pisten�yt�n sijainti sen korkeuden mukaan
            entryTransform.SetActive(true); // N�ytet��n pisten�ytt�

            string sijoitusTeksti = HaeSijoitusTeksti(i + 1); // Haetaan sijoituksen tekstiesitys
            entryTransform.transform.Find("posText").GetComponent<TMPro.TMP_Text>().text = sijoitusTeksti; // Asetetaan sijoituksen teksti pisten�ytt��n

            entryTransform.transform.Find("scoreText").GetComponent<TMPro.TMP_Text>().text = scores[i].score.ToString(); // Asetetaan pistem��r� pisten�ytt��n
            entryTransform.transform.Find("nameText").GetComponent<TMPro.TMP_Text>().text = scores[i].name; // Asetetaan pelaajan nimi pisten�ytt��n
        }
    }

    private string HaeSijoitusTeksti(int sijoitus)
    {
        if (sijoitus == 1) return "1ST";
        if (sijoitus == 2) return "2ND";
        if (sijoitus == 3) return "3RD";
        return sijoitus + "TH";
    }

    // Tallentaa p�ivitetyn pistetaulukon pelaajan tilap�ismuistiin
    public void SaveHighScores()
    {
        for (int i = 0; i < scores.Count; i++)
        {
            PlayerPrefs.SetInt("HighScore" + i, scores[i].score); // Tallentaa pistem��r�n pelaajan tilap�ismuistiin
            PlayerPrefs.SetString("Name" + i, scores[i].name); // Tallentaa pelaajan nimen pelaajan tilap�ismuistiin
        }
    }

    // Lis�� uuden pistem��r�n ja pelaajan nimen pistetaulukkoon
    public void AddNewScore(string playerName, int score)
    {
        // Luo uusi pistem��r�merkint�
        ScoreEntry newEntry = new ScoreEntry { name = playerName, score = score };

        // Lis�� uusi merkint� pistetaulukkoon
        scores.Add(newEntry);

        // J�rjest� pistem��r�t pit��ksesi j�rjestyksen
        scores.Sort((a, b) => b.score.CompareTo(a.score));

        // Valinnaisesti voit p�ivitt�� k�ytt�liittym�� pistetaulukon muutosten mukaisesti
        AlustaPisteTaulukko();

        // Tallenna pistetaulukko
        SaveHighScores();
    }
}
