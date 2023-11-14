using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class Tukki : MonoBehaviour
{
    // Alkuper�inen tukin ilmestymisv�li minimi ja maksimi
    public float initialSpawnIntervalMin = 6f;
    public float initialSpawnIntervalMax = 5f;

    // Korkeus, jossa tukki poistetaan
    public float despawnKorkeus = -10f;

    // Tukin liikkumisnopeus
    private float nopeus = 5f;

    // Tera-peliobjekti ja tuhoet�isyys
    private Transform tera;

    public float tuhoetaisyys = 1.0f;

    // Muuttuja, joka tallentaa ter�n nimen
    public string teranNimi = "Tera";

    private void Start()
    {
        
    }

    private void Update()
    {
        // Liikutetaan objektia taaksep�in nopeuden verran
        transform.Translate(Vector3.back * nopeus * Time.deltaTime);

        // Etsit��n ter�-objekti dynaamisesti sen nimen perusteella
        if (tera == null)
        {
            GameObject teraObj = GameObject.Find(teranNimi);

            if (teraObj != null)
            {
                tera = teraObj.transform;
            }
            else
            {
                Debug.LogError("Tera-objektia ei l�ytynyt. Tarkista nimi ja varmista, ett� se on aktiivinen.");
                return;
            }
        }

        // Lasketaan et�isyys tukin ja ter�n v�lill�
        float etaisyys = Vector3.Distance(transform.position, tera.position);

        // Tarkistetaan, onko et�isyys alle tuhoet�isyyden ja tarkistetaan y-akseli
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

        // Jos tukki saavuttaa m��ritetyn korkeuden, peli p��ttyy
        if (transform.position.z <= despawnKorkeus)
        {
            // Tukki tuhoutuu
            Destroy(gameObject);


            // Tulostetaan peli p��ttyneeksi
            Debug.Log("Game Over");

            // Siirryt��n seuraavaan pelisceneen
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}