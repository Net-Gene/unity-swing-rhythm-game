using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Tukki : MonoBehaviour
{
    
    public float initialSpawnIntervalMin = 6f; // Alkuper‰inen tukin ilmestymisv‰li minimiarvo
    public float initialSpawnIntervalMax = 5f; // Alkuper‰inen tukin ilmestymisv‰li maksimiarvo
    public float despawnKorkeus = -10f; // Korkeus, jossa tukki poistetaan
    public GameObject pelaaja; // Pelaajan peliobjekti
    public GameObject kirves; // Kirveksen peliobjekti
    private float nopeus = 5f; // Tukin liikkumisnopeus

    public Transform tera; // Asetaan "Tera" peliobjektiinsa t‰h‰n
    public float tuhoetaisyys = 1.0f; // Asetetaan tuhoet‰isyys

    public NykyinenScore nykyinenScore; // asetetaan nykyinen score
    

    void Start()
    {
        pelaaja = GameObject.Find("Pelaaja"); // Etsit‰‰n pelaajan peliobjekti
        kirves = GameObject.Find("Kirves"); // Etsit‰‰n kirveksen peliobjekti
        nykyinenScore = GameObject.FindObjectOfType<NykyinenScore>();
    }

    void Update()
    {
        
        // Liikutetaan objektia eteenp‰in
        transform.Translate(Vector3.back * nopeus * Time.deltaTime);
        

        // Lasketaan et‰isyys tukin ja kirveen ter‰n v‰lill‰ 
        float etaisyys = Vector3.Distance(transform.position, tera.position);
        
        // Tarkistetaan, onko et‰isyys alle tuhoet‰isyyden y- ja z-akseleilla 
        if (etaisyys < tuhoetaisyys && Mathf.Abs(transform.position.y - tera.position.y) < 1.0f)
        {
            // Tukki on tarpeeksi l‰hell‰ "Tera", tuhotaan se 
            Destroy(gameObject);
            nykyinenScore.LisaaPisteita(10f);
        }
        

        // Jos tukki p‰‰see luppuun, pelaaja h‰vi‰‰
        if (transform.position.z <= despawnKorkeus)
        {
            Destroy(gameObject);
            Debug.Log("Game Over");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

    }

    // Vanha ep‰toiminnollinen kirveeseen osunta script
    /*
    // Lis‰t‰‰n tukille mahdollisuus tarkistaa osuma Kirvekseen
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Kirves"))
        {
            Destroy(gameObject); // Tukki poistetaan, kun se osuu Kirvekseen
        }
    }
    */
}

