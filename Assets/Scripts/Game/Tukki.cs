using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


public class Tukki : MonoBehaviour
{
    public float initialSpawnIntervalMin = 6f; // Alkuperäinen tukin ilmestymisväli minimiarvo
    public float initialSpawnIntervalMax = 5f; // Alkuperäinen tukin ilmestymisväli maksimiarvo
    public float despawnKorkeus = -10f; // Korkeus, jossa tukki poistetaan
    public GameObject pelaaja; // Pelaajan peliobjekti
    public GameObject kirves; // Kirveksen peliobjekti
    private float nopeus = 5f; // Tukin liikkumisnopeus

    public Transform tera; // Asetaan "Tera" peliobjektiinsa tähän
    public float tuhoetaisyys = 1.0f; // Asetetaan tuhoetäisyys

    void Start()
    {
        pelaaja = GameObject.Find("Pelaaja"); // Etsi pelaajan peliobjekti
        kirves = GameObject.Find("Kirves"); // Etsi kirveksen peliobjekti
    }

    void Update()
    {
        // Liikutetaan tukkia eteenpäin
        transform.Translate(Vector3.back * nopeus * Time.deltaTime);

        // Lasketaan etäisyys tukin ja kirveen terän välillä 
        float etaisyys = Vector3.Distance(transform.position, tera.position);

        // Tarkistetaan, onko etäisyys alle tuhoetäisyyden y- ja z-akseleilla 
        if (etaisyys < tuhoetaisyys && Mathf.Abs(transform.position.y - tera.position.y) < 1.0f)
        {
            // Tukki on tarpeeksi lähellä "Tera", tuhotaan se 
            Destroy(gameObject);
        }

        // Tarkista, onko tukki liian alhaalla ja poistetaan
        if (transform.position.z <= despawnKorkeus)
        {
            Destroy(gameObject);
            Debug.Log("Gmae Over");
        }

    }

    // Lisätään tukille mahdollisuus tarkistaa osuma Kirvekseen
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Kirves"))
        {
            Destroy(gameObject); // Tukki poistetaan, kun se osuu Kirvekseen
        }
    }
}

