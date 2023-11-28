using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// HiScore
/// </summary>
public class HiScore : MonoBehaviour
{
    /// <summary>
    /// hiScoreTable
    /// </summary>
    [SerializeField] private GameObject hiScoreTable;

    /// <summary>
    /// hiScoreElement
    /// </summary>
    [SerializeField] private GameObject hiScoreElement;

    /// <summary>
    /// hiScoreBoard
    /// </summary>
    [SerializeField] private GameObject hiScoreBoard;

    /// <summary>
    /// hiScoreInput
    /// </summary>
    [SerializeField] private GameObject hiScoreInput;

    /// <summary>
    /// HiScoreElement
    /// </summary>
    List<HiScoreElement> list = new List<HiScoreElement>();

    /// <summary>
    /// hiScoreStore
    /// </summary>
    HiScoreList hiScoreStore;

    /// <summary>
    /// instance
    /// </summary>
    private static HiScore instance;

    /// <summary>
    /// Instance
    /// </summary>
    public static HiScore Instance
    {
        get
        {
            return instance;
        }
    }

    /// <summary>
    /// Start is called on the frame when a script is enabled just before any of the Update methods are called the first time.
    /// </summary>
    void Start()
    {
        // Asetetaan instanssi itseensä
        instance = this;

        // Luetaan pistetaulukko
        hiScoreStore = ReadScore();

        // Päivitetään pistetaulukko
        UpdateScoreBoard(hiScoreStore.HiScoreElementList);

        // Näytetään pistetaulukko
        Instance.Show();
    }

    /// <summary>
    /// Näytä pistetaulukko
    /// </summary>
    public void Show()
    {
        hiScoreBoard.SetActive(true);
    }

    /// <summary>
    /// Piilota pistetaulukko
    /// </summary>
    public void Hide()
    {
        hiScoreBoard.SetActive(false);
    }

    /// <summary>
    /// Päivitä pistetaulukko
    /// </summary>
    /// <param name="list"></param>
    void UpdateScoreBoard(List<HiScoreElement> list)
    {
        // Tyhjennä vanhat pistetaulukon elementit
        foreach (Transform tf in hiScoreTable.transform)
        {
            Destroy(tf.gameObject);
        }

        // Luo uudet pistetaulukon elementit annetun listan perusteella
        foreach (HiScoreElement el in list)
        {
            GameObject newScore = GameObject.Instantiate(hiScoreElement, hiScoreTable.transform);

            // Aktivoi uusi pistetaulukon elementti
            newScore.SetActive(true);

            // Aseta tekstin arvo annetun HiScoreElementin mukaiseksi
            newScore.GetComponent<Text>().text = el.Name + " " + el.Score;
        }
    }

    /// <summary>
    /// Tallenna uusi pistetulos
    /// </summary>
    /// <param name="name"></param>
    /// <param name="score"></param>
    public void Save(string name, float score)
    {
        // Luo uusi HiScoreElement tallennettavalla tiedolla
        HiScoreElement newScoreElement = new HiScoreElement(name, score);

        // Lisää uusi HiScoreElement pistetaulukkoon
        hiScoreStore.AddToList(newScoreElement);

        // Tallenna pistetaulukko
        SaveScoreBoard(hiScoreStore);

        // Piilota pistetaulukon syöttökenttä
        hiScoreInput.gameObject.SetActive(false);

        // Päivitä pistetaulukko näytölle
        UpdateScoreBoard(hiScoreStore.HiScoreElementList);
    }

    /// <summary>
    /// Tallenna pistetaulukko
    /// </summary>
    /// <param name="sb"></param>
    public void SaveScoreBoard(HiScoreList sb)
    {
        // Tulosta tallennustiedoston polku debug-lokiin
        Debug.Log(Application.persistentDataPath + "/hiScoreData.dat");

        // Luo uusi BinaryFormatter
        BinaryFormatter bf = new BinaryFormatter();

        // Avaa tai luo tiedosto tallennusta varten
        FileStream file = File.Open(Application.persistentDataPath + "/hiScoreData.dat", FileMode.OpenOrCreate);

        // Serialisoi ja kirjoita HiScoreList tiedostoon
        bf.Serialize(file, sb);

        // Sulje tiedostovirta
        file.Close();
    }

    /// <summary>
    /// Lue pistetaulukko tiedostosta
    /// </summary>
    /// <returns></returns>
    HiScoreList ReadScore()
    {
        // Alusta pistetaulukko
        HiScoreList sb = null;

        // Luo uusi BinaryFormatter
        BinaryFormatter bf = new BinaryFormatter();

        // Avaa tallennustiedosto
        FileStream file = File.Open(Application.persistentDataPath + "/s002.save", FileMode.Open);

        // Deserialisoi tiedostosta HiScoreList
        sb = (HiScoreList)bf.Deserialize(file);

        // Sulje tiedostovirta
        file.Close();

        // Jos pistetaulukkoa ei ole vielä olemassa, luo uusi ja tallenna se
        sb = new HiScoreList();
        sb.HiScoreElementList = new List<HiScoreElement>();
        SaveScoreBoard(sb);

        // Tulosta pistetaulukon sisältö debug-lokiin
        Debug.Log("TULOSTETAAN PISTETTAULUKKO");
        foreach (HiScoreElement e in sb.HiScoreElementList)
        {
            Debug.Log(e.Name + " - " + e.Score);
        }

        return sb;
    }
}
