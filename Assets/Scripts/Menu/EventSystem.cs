using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ChangeFirstSelected : MonoBehaviour
{
    public Button yourButton; // Assign your button in the Inspector

    public GameObject newFirstSelectedObject; // Assign the new object in the Inspector

    private EventSystem eventSystem;

    private void Start()
    {
        eventSystem = FindObjectOfType<EventSystem>();
        yourButton.onClick.AddListener(ChangeFirstSelectedObject);
    }

    public void ChangeFirstSelectedObject()
    {
        Debug.Log("ChangeFirstSelectedObject method called.");

        if (eventSystem != null)
        {
            eventSystem.firstSelectedGameObject = newFirstSelectedObject;
        }
        else
        {
            Debug.LogError("EventSystem not found in the scene.");
        }

        Debug.Log("ChangeFirstSelectedObject method completed.");
    }

}
