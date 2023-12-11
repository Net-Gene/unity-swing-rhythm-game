using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class Tukki : MonoBehaviour
{
    // Korkeus, jossa tukki poistetaan
    private float despawnKorkeus = -10f;

    // Tukin liikkumisnopeus
    private float nopeus = 5f;

    // Tera-peliobjekti ja tuhoet�isyys
    private Transform tera;

    private float tuhoetaisyys = 1.0f;

    // Muuttuja, joka tallentaa ter�n nimen
    private string teranNimi = "Tera";

    public GameObject FloatingPoints;

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
        if (etaisyys < tuhoetaisyys)
        {
            // Tukki tuhoutuu
            Destroy(gameObject);

            // Floating points txt spawn when game object is destroyed
            Instantiate(FloatingPoints, new Vector3(transform.position.x, transform.position.y + 1.5f, transform.position.z), Quaternion.identity);

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