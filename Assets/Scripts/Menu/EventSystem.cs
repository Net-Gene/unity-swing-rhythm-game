using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ChangeFirstSelected : MonoBehaviour
{
    // Aseta painikkeesi Inspectorissa
    public Button yourButton;

    // Aseta uusi objekti Inspectorissa
    public GameObject newFirstSelectedObject;

    private EventSystem eventSystem;

    private void Start()
    {
        // Etsi EventSystemin instanssi
        eventSystem = FindObjectOfType<EventSystem>();

        // Lis?? kuuntelija nappulan klikkaukselle
        yourButton.onClick.AddListener(ChangeFirstSelectedObject);
    }

    // Metodi ensimm?isen valitun objektin vaihtamiseksi
    public void ChangeFirstSelectedObject()
    {
        Debug.Log("ChangeFirstSelectedObject method called.");

        // Tarkista, onko eventSystem olemassa
        if (eventSystem != null)
        {
            // Vaihda ensimm?isen valitun peliobjektin arvo uuteen objektiin
            eventSystem.firstSelectedGameObject = newFirstSelectedObject;
            eventSystem.SetSelectedGameObject(newFirstSelectedObject);
           newFirstSelectedObject.GetComponent<Button>().onClick.AddListener(ChangeFirstSelectedObject);


        }
        else
        {
            Debug.LogError("EventSystem not found in the scene.");
        }

        Debug.Log("ChangeFirstSelectedObject method completed.");
    }
}
