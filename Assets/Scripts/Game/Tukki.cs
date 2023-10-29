using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


public class Tukki : MonoBehaviour
{
    public float initialSpawnIntervalMin = 6f; // Alkuper�inen tukin ilmestymisv�li minimiarvo
    public float initialSpawnIntervalMax = 5f; // Alkuper�inen tukin ilmestymisv�li maksimiarvo
    public float despawnKorkeus = -10f; // Korkeus, jossa tukki poistetaan
    public GameObject pelaaja; // Pelaajan peliobjekti
    public GameObject kirves; // Kirveksen peliobjekti
    private float nopeus = 5f; // Tukin liikkumisnopeus

    void Start()
    {
        pelaaja = GameObject.Find("Pelaaja"); // Etsi pelaajan peliobjekti
        kirves = GameObject.Find("Kirves"); // Etsi kirveksen peliobjekti
    }

    void Update()
    {
        // Liikutetaan tukkia eteenp�in
        transform.Translate(Vector3.back * nopeus * Time.deltaTime);

        // Tarkistetaan onko tukki osunut pelaajan hitboxiin
        if (transform.position.z <= pelaaja.transform.position.z)
        {
            if (transform.position.x == pelaaja.transform.position.x)
            {
                // Pelaaja ei ly�nyt tukkia oikeaan aikaan, pelaaja h�vi��
                Debug.Log("You lost");
                Destroy(gameObject); // Tukki poistetaan, kun se osuu pelaajaan
            }
        }

        // Tarkista, onko tukki liian alhaalla ja poistetaan
        if (transform.position.z <= despawnKorkeus)
        {
            Destroy(gameObject);
        }
    }

    // Lis�t��n tukille mahdollisuus tarkistaa osuma Kirvekseen
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Kirves"))
        {
            Destroy(gameObject); // Tukki poistetaan, kun se osuu Kirvekseen
        }
    }
}

