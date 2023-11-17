using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

// M‰‰ritet‰‰n luokka HiScoreInputController, joka perii MonoBehaviourin
public class HiScoreInputController : MonoBehaviour
{
    // M‰‰ritet‰‰n GameObject hiScoreInput, joka voidaan asettaa Inspectorissa
    [SerializeField] private GameObject hiScoreInput;

    // M‰‰ritet‰‰n staattinen HiScoreInputController-instanssi
    private static HiScoreInputController instance;

    // Oletetaan, ett‰ HiScoreList on serialisoitu luokka
    public static HiScoreList hiScoreStore = new HiScoreList();

    // Luodaan staattinen omaisuus Instance, joka antaa p‰‰syn HiScoreInputController-instanssiin
    public static HiScoreInputController Instance
    {
        get { return instance; }
        set { instance = value; }
    }

    // Alustetaan skripti
    void Start()
    {
        instance = this;

        // Ladataan mahdollinen olemassa oleva data
        hiScoreStore = ReadScore();

        // Ensure that HiScoreInputController.Instance is set correctly
        if (instance != null)
        {
            HiScoreInputController.Instance = this;
        }
    }

    // Tallennetaan uusi piste
    public void Save(string name, float score)
    {
        if (HiScoreInputController.Instance != null)
        {
            if (HiScoreInputController.hiScoreStore != null)
            {
                // Luodaan uusi HiScoreElement ja lis‰t‰‰n se listaan
                HiScoreElement newScoreElement = new HiScoreElement(name, score);
                hiScoreStore.AddToList(newScoreElement);

                // Tallennetaan pistetaulukko
                SaveScoreBoard(hiScoreStore);

                // P‰ivitet‰‰n pistetaulukko HiScoreBoardController-instanssin kautta
                HiScoreBoardController.Instance.UpdateScoreBoard(hiScoreStore.HiScoreElementList);

            }
            else
            {
                Debug.LogError("HiScoreInputController.hiScoreStore is null. Make sure it is properly initialized.");
            }
        }
        else
        {
            Debug.LogError("HiScoreInputController.Instance is null. Make sure it is properly initialized.");
        }
    }

    // Tallennetaan pistetaulukko
    public static void SaveScoreBoard(HiScoreList sb)
    {
        // Luodaan bin‰‰rimuotoilija ja avataan tai luodaan tiedosto
        BinaryFormatter bf = new BinaryFormatter();
        Debug.Log("Persistent Data Path: " + Application.persistentDataPath);
        FileStream file = File.Open(Application.persistentDataPath + "/hiScoreData.dat", FileMode.OpenOrCreate);

        // Serialisoidaan ja kirjoitetaan HiScoreList-tiedosto
        bf.Serialize(file, sb);

        // Suljetaan tiedostovirta
        file.Close();
    }

    // Luetaan tallennettu pistetaulukko
    HiScoreList ReadScore()
    {
        // Alustetaan pistetaulukko-olio
        HiScoreList sb = null;

        // Tarkistetaan, onko tallennettua tiedostoa olemassa
        if (File.Exists(Application.persistentDataPath + "/hiScoreData.dat"))
        {
            // Luodaan bin‰‰rimuotoilija ja avataan tiedosto
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/hiScoreData.dat", FileMode.Open);

            // Deserialisoidaan tiedosto HiScoreList-olioksi
            sb = (HiScoreList)bf.Deserialize(file);

            // Suljetaan tiedostovirta
            file.Close();

            // Palautetaan luetut tiedot
            return sb;
        }
        else
        {
            // Jos tiedostoa ei ole, luodaan uusi pistetaulukko
            sb = new HiScoreList();
            sb.HiScoreElementList = new List<HiScoreElement>();

            // Alustetaan joitakin oletusarvoja
            // AddToList-metodi k‰sittelee elementtien lis‰‰misen listaan
            SaveScoreBoard(sb);
            Debug.Log("Oletusarvot HiScoreListille luotu.");
            return sb;
        }
    }

    // Kommentoitu ShowInputQuery-metodi
    /*
    public void ShowInputQuery(float scoreU)
    {
        if (hiScoreInput != null)
        {
            hiScoreInput.GetComponent<HiScoreInput>().score = scoreU;
            hiScoreInput.SetActive(true);
        }
        else
        {
            Debug.LogError("HiScoreInput not found! Make sure the object name is correct.");
        }
        hiScoreInput.gameObject.SetActive(true);
    }
    */
}
