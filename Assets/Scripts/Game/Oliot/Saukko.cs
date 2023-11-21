using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class Saukko : MonoBehaviour
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


    private void Update()
    {
        // Liikutetaan objektia eteenp�in nopeuden verran
        transform.Translate(Vector3.forward * nopeus * Time.deltaTime);

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

        // Lasketaan et�isyys kalan ja ter�n v�lill�
        float etaisyys = Vector3.Distance(transform.position, tera.transform.position);
        // Tarkistetaan, onko et�isyys alle tuhoet�isyyden ja tarkistetaan y-akseli
        if (etaisyys < tuhoetaisyys)
        {
            // Kala tuhoutuu
            Destroy(gameObject);

            // Lis�tt�v�t pisteet
            int value = 200;
            GameLogic.score -= value;
            // Tulostetaan pisteet konsoliin
            Debug.Log("Pisteet: " + GameLogic.score);

            // Tulostetaan peli p��ttyneeksi
            Debug.Log("Game Over");

            // Siirryt��n seuraavaan pelisceneen
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

        // Kala despawnaa, Jos se saavuttaa m��ritetyn korkeuden
        if (transform.position.z <= despawnKorkeus)
        {
            // Kala tuhoutuu
            Destroy(gameObject);

            // Lis�tt�v�t pisteet
            int value = 200;
            GameLogic.score += value;

            // Tulostetaan pisteet konsoliin
            Debug.Log("Pisteet: " + GameLogic.score);
        }
    }
}