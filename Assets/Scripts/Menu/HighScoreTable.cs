using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class HighScoreTable : MonoBehaviour
{
    public GameObject entryContainer; // Aseta Inspectorissa
    public GameObject entryTemplate; // Aseta Inspectorissa
    public TMPro.TMP_Text highScoreText; // Aseta Inspectorissa
    public List<int> scores = new List<int>(); // Säilytä useita pisteitä

    private void Start()
    {

        // Lisää nykyiset pisteet listaan
        scores.Add(PlayerPrefs.GetInt("CurrentScore", 0));

        // Sort the scores in descending order
        scores.Sort((a, b) => b.CompareTo(a));

        AlustaPisteTaulukko();

        // Hae enimmäispisteet PlayerPrefs:ista ja näytä ne
        int enimmäispisteet = PlayerPrefs.GetInt("Highscore", 0);
        highScoreText.text = enimmäispisteet.ToString();
    }

    private void AlustaPisteTaulukko()
    {
        entryTemplate.SetActive(false);

        float pohjapohjanKorkeus = 20f;

        for (int i = 0; i < scores.Count; i++)
        {
            GameObject entryTransform = Instantiate(entryTemplate, entryContainer.transform);
            RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
            entryRectTransform.anchoredPosition = new Vector2(0, -pohjapohjanKorkeus * i);
            entryTransform.SetActive(true);

            int sijoitus = i + 1;
            string sijoitusTeksti = HaeSijoitusTeksti(sijoitus);
            entryTransform.transform.Find("posText").GetComponent<TMPro.TMP_Text>().text = sijoitusTeksti;

            // Näytä jokainen piste
            int pisteet = scores[i];
            entryTransform.transform.Find("scoreText").GetComponent<TMPro.TMP_Text>().text = pisteet.ToString();

            string staattinenNimi = "AAA"; // Aseta tallennettaville pisteille staattinen nimi
            entryTransform.transform.Find("nameText").GetComponent<TMPro.TMP_Text>().text = staattinenNimi;
        }
    }

    private string HaeSijoitusTeksti(int sijoitus)
    {
        if (sijoitus == 1) return "1ST";
        if (sijoitus == 2) return "2ND";
        if (sijoitus == 3) return "3RD";
        return sijoitus + "TH";
    }
}
