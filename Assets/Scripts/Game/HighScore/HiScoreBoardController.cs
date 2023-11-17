using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class HiScoreBoardController : MonoBehaviour
{
    // HiScoreBoardController vastaa huipputulostaulusta peliss‰.

    [SerializeField] private GameObject hiScoreTable; // GameObject huipputulostaululle.
    [SerializeField] private GameObject hiScoreElement; // GameObject yksitt‰iselle huipputulokselle.
    [SerializeField] private GameObject hiScoreBoard; // GameObject koko huipputulostaulun alueelle.

    private static HiScoreBoardController instance; // Staattinen instanssi HiScoreBoardControllerista.

    public static HiScoreBoardController Instance
    {
        get { return instance; }
    }

    void Start()
    {
        // Aseta instanssi itseens‰ ja lataa huipputulostaulun tiedot.
        instance = this;
        HiScoreInputController.hiScoreStore = ReadScore();
        UpdateScoreBoard(HiScoreInputController.hiScoreStore.HiScoreElementList);
        Instance.Show();
    }

    public void Show()
    {
        // N‰yt‰ huipputulostaulu.
        hiScoreBoard.SetActive(true);
    }

    public void Hide()
    {
        // Piilota huipputulostaulu.
        hiScoreBoard.SetActive(false);
    }

    public void UpdateScoreBoard(List<HiScoreElement> list)
    {
        // P‰ivit‰ huipputulostaulu annetulla listalla.

        // Tarkista, ett‰ huipputaulu on m‰‰ritelty inspectorissa.
        if (hiScoreTable == null)
        {
            Debug.LogError("hiScoreTable ei ole m‰‰ritelty inspectorissa.");
            return;
        }

        // Poista kaikki huipputaulun lapsiobjektit.
        foreach (Transform tf in hiScoreTable.transform)
        {
            Destroy(tf.gameObject);
        }

        // Luo uudet huipputulosobjektit annetun listan perusteella.
        foreach (HiScoreElement el in list)
        {
            GameObject newScore = Instantiate(hiScoreElement, hiScoreTable.transform);
            newScore.SetActive(true);
            newScore.GetComponent<Text>().text = el.Name + " " + el.Score;
        }
    }

    HiScoreList ReadScore()
    {
        // Lue tallennetut huipputulokset tiedostosta.

        HiScoreList sb = null;

        // Tarkista, onko tallennettua dataa.
        if (File.Exists(Application.persistentDataPath + "/hiScoreData.dat"))
        {
            // Lue tallennettu data tiedostosta.
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/hiScoreData.dat", FileMode.Open);
            sb = (HiScoreList)bf.Deserialize(file);
            file.Close();

            return sb;
        }
        else
        {
            // Luo uusi huipputulostaulu, jos tallennettua dataa ei ole.
            sb = new HiScoreList();
            sb.HiScoreElementList = new List<HiScoreElement>();
            HiScoreInputController.SaveScoreBoard(sb);  // Varmista, ett‰ olet tallentanut oletusarvoisen listan
            Debug.Log("Oletusarvoinen Huipputulostaulu luotu.");
            return sb;
        }
    }
}
