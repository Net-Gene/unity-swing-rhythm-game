using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class Tukki : MonoBehaviour
{
    // Alkuperäinen tukin ilmestymisväli minimi ja maksimi
    public float initialSpawnIntervalMin = 6f;
    public float initialSpawnIntervalMax = 5f;

    // Korkeus, jossa tukki poistetaan
    public float despawnKorkeus = -10f;

    // Tukin liikkumisnopeus
    private float nopeus = 5f;

    // Tera-peliobjekti ja tuhoetäisyys
    private Transform tera;

    public float tuhoetaisyys = 1.0f;

    // Muuttuja, joka tallentaa terän nimen
    public string teranNimi = "Tera";

    private void Start()
    {
        
    }

    private void Update()
    {
        // Liikutetaan objektia taaksepäin nopeuden verran
        transform.Translate(Vector3.back * nopeus * Time.deltaTime);

        // Etsitään terä-objekti dynaamisesti sen nimen perusteella
        if (tera == null)
        {
            GameObject teraObj = GameObject.Find(teranNimi);

            if (teraObj != null)
            {
                tera = teraObj.transform;
            }
            else
            {
                Debug.LogError("Tera-objektia ei löytynyt. Tarkista nimi ja varmista, että se on aktiivinen.");
                return;
            }
        }

        // Lasketaan etäisyys tukin ja terän välillä
        float etaisyys = Vector3.Distance(transform.position, tera.position);

        // Tarkistetaan, onko etäisyys alle tuhoetäisyyden ja tarkistetaan y-akseli
        if (etaisyys < tuhoetaisyys && Mathf.Abs(transform.position.y - tera.position.y) < 1.0f)
        {
            // Tukki tuhoutuu
            Destroy(gameObject);

            // Lasketaan pisteet pelilogiikan perusteella
            int value = 15;
            GameLogic.score += value;



            // Tulostetaan pisteet konsoliin
            Debug.Log("Pisteet: " + GameLogic.score);
        }

        // Jos tukki saavuttaa määritetyn korkeuden, peli päättyy
        if (transform.position.z <= despawnKorkeus)
        {
            // Tukki tuhoutuu
            Destroy(gameObject);


            // Tulostetaan peli päättyneeksi
            Debug.Log("Game Over");

            // Siirrytään seuraavaan pelisceneen
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}