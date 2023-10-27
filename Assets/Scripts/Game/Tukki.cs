using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


public class Tukki : MonoBehaviour
{
    public float initialSpawnIntervalMin = 6f;
    public float initialSpawnIntervalMax = 5f;
    public float despawnKorkeus = -10f;
    public GameObject pelaaja;
    public GameObject kirves;
    private float nopeus = 5f;

    void Start()
    {
        pelaaja = GameObject.Find("Pelaaja"); // Pelaaja voidaan paikata myös joen lopussa olevalla hitboksilla
        kirves = GameObject.Find("Kirves"); // etitään kirves
    }


    void Update()
    {
        // Liikutetaan tukkia eteenpäin
        transform.Translate(Vector3.back * nopeus * Time.deltaTime);

        // Tarkistetaan onko tukki osunut pelaajan hitboxiin
        if (transform.position.z <= pelaaja.transform.position.z)
        {
            if (transform.position.x == pelaaja.transform.position.x)
            {
                // Pelaaja ei lyönyt tukkia oikeaan aikaan, pelaaja häviää 
                print("Hävisit");
                Destroy(gameObject); // Tukki despawnataan, kun se osuu pelaajaan
            }
        }

        // Tarkista, onko tukki liian alhaalla ja despawnataan
        if (transform.position.z <= despawnKorkeus)
        {
            Destroy(gameObject);
        }
    }

    // Lisätään tukille mahdollisuus tarkistaa osuma Kirvekseen
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Kirves"))
        {
            Destroy(gameObject); // Tukki despawnaa, kun se osuu Kirvekseen
        }
    }

}

