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

    public Transform tera; // Asetaan "Tera" peliobjektiinsa t�h�n
    public float tuhoetaisyys = 1.0f; // Asetetaan tuhoet�isyys

    void Start()
    {
        pelaaja = GameObject.Find("Pelaaja"); // Etsi pelaajan peliobjekti
        kirves = GameObject.Find("Kirves"); // Etsi kirveksen peliobjekti
    }

    void Update()
    {
        // Liikutetaan tukkia eteenp�in
        transform.Translate(Vector3.back * nopeus * Time.deltaTime);

        // Lasketaan et�isyys tukin ja kirveen ter�n v�lill� 
        float etaisyys = Vector3.Distance(transform.position, tera.position);

        // Tarkistetaan, onko et�isyys alle tuhoet�isyyden y- ja z-akseleilla 
        if (etaisyys < tuhoetaisyys && Mathf.Abs(transform.position.y - tera.position.y) < 1.0f)
        {
            // Tukki on tarpeeksi l�hell� "Tera", tuhotaan se 
            Destroy(gameObject);
        }

        // Tarkista, onko tukki liian alhaalla ja poistetaan
        if (transform.position.z <= despawnKorkeus)
        {
            Destroy(gameObject);
            Debug.Log("Gmae Over");
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

