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
        instance = this;


        hiScoreStore = ReadScore();


        UpdateScoreBoard(hiScoreStore.HiScoreElementList);

        Instance.Show();

    }

    /// <summary>
    /// Show Hiscore Board
    /// </summary>
    public void Show()
    {
        hiScoreBoard.SetActive(true);
    }

    /// <summary>
    /// Hide Hiscore Board
    /// </summary>
    public void Hide()
    {
        hiScoreBoard.SetActive(false);
    }


    /// <summary>
    /// UpdateScoreBoard
    /// </summary>
    /// <param name="aList"></param>
    void UpdateScoreBoard(List<HiScoreElement> list)
    {
        foreach(Transform tf in hiScoreTable.transform)
        {
            Destroy(tf);
        }

        
        foreach(HiScoreElement el in list)
        {
            GameObject newScore = GameObject.Instantiate(hiScoreElement, hiScoreTable.transform);
            
            newScore.SetActive(true);

            newScore.GetComponent<Text>().text = el.Name + " " + el.Score;
        }


    }


    /// <summary>
    /// Save
    /// </summary>
    /// <param name="name"></param>
    /// <param name="score"></param>
    public void Save(string name, float score)
    {
        HiScoreElement newScoreElement = new HiScoreElement(name, score);
        hiScoreStore.AddToList(newScoreElement);

        SaveScoreBoard(hiScoreStore);

        hiScoreInput.gameObject.SetActive(false);

        UpdateScoreBoard(hiScoreStore.HiScoreElementList);
    }

    /*
    /// <summary>
    /// ShowInputQuery
    /// </summary>
    /// <param name="score"></param>
    public void ShowInputQuery(float scoreU)
    {
        GameObject hiScoreInput = GameObject.Find("SetHiScore");

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


    /// <summary>
    /// SaveScoreBoard
    /// </summary>
    /// <param name="sb"></param>
    public void SaveScoreBoard(HiScoreList sb)
    {

        Debug.Log(Application.persistentDataPath + "/hiScoreData.dat");
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Open(Application.persistentDataPath + "/hiScoreData.dat",FileMode.OpenOrCreate);

        // Serialize and write the HiScoreList to the file
        bf.Serialize(file, sb);

        // Close the file stream
        file.Close();

    }


    /// <summary>
    /// ReadScore
    /// </summary>
    /// <returns></returns>
    HiScoreList ReadScore()
        {
            HiScoreList sb = null;

            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/s002.save", FileMode.Open);
            sb = (HiScoreList)bf.Deserialize(file);
            file.Close();
            sb = new HiScoreList();
            sb.HiScoreElementList = new List<HiScoreElement>();
            SaveScoreBoard(sb);

            Debug.Log("TULOSTETAAN PISTETTAULUKKO");
            foreach (HiScoreElement e in sb.HiScoreElementList)
                {
                    Debug.Log(e.Name + " - " + e.Score);
                }
            return sb;
            /*
            if (File.Exists(Application.persistentDataPath + "/s002.save"))
            {

            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/s002.save", FileMode.Open);
            sb = (HiScoreList)bf.Deserialize(file);
            file.Close();

            return sb;

        }
        else
        {


            sb = new HiScoreList();
            sb.HiScoreElementList = new List<HiScoreElement>();
            sb.AddToList(new HiScoreElement("ABC", 15));
            sb.AddToList(new HiScoreElement("CCC", 10));
            sb.AddToList(new HiScoreElement("DDD", 5));
            sb.AddToList(new HiScoreElement("EEE", 2));
            sb.AddToList(new HiScoreElement("FFF", 200));
            sb.AddToList(new HiScoreElement("GGG", 20));
            sb.AddToList(new HiScoreElement("HHH", 1));
            sb.AddToList(new HiScoreElement("III", 60));
            sb.AddToList(new HiScoreElement("JJJ", 3));
            sb.AddToList(new HiScoreElement("KKK", 7));
            sb.AddToList(new HiScoreElement("LLL", 4));
            SaveScoreBoard(sb);

            Debug.Log("TULOSTETAAN PISTETTAULUKKO");
            foreach (HiScoreElement e in sb.HiScoreElementList)
            {
                Debug.Log(e.Name + " - " + e.Score);
            }
            return sb;
        }*/
    }
}
