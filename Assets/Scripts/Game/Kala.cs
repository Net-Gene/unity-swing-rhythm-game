using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class Kala : MonoBehaviour
{
    // Alkuper�inen tukin ilmestymisv�li minimi ja maksimi
    public float initialSpawnIntervalMin = 6f;
    public float initialSpawnIntervalMax = 5f;

    // Korkeus, jossa tukki poistetaan
    public float despawnKorkeus = -10f;

    // Tukin liikkumisnopeus
    private float nopeus = 5f;

    // Tera-peliobjekti ja tuhoet�isyys
    public Transform tera;

    public float tuhoetaisyys = 1.0f;

    // Muuttuja, joka tallentaa ter�n nimen
    public string teranNimi = "Tera";


    void Start()
    {
    }


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
        Debug.LogError(etaisyys);
        // Tarkistetaan, onko et�isyys alle tuhoet�isyyden ja tarkistetaan y-akseli
        if (etaisyys < tuhoetaisyys /*&& Mathf.Abs(transform.position.y - tera.position.y) < 1*/)
        {
            // Kala tuhoutuu
            Destroy(gameObject);

            // Lis�tt�v�t pisteet
            int value = 15;
            GameLogic.score -= value;
            // Tulostetaan pisteet konsoliin
            Debug.Log("Pisteet: " + GameLogic.score);
        }

        // Kala despawnaa, Jos se saavuttaa m��ritetyn korkeuden
        if (transform.position.z <= despawnKorkeus)
        {
            // Kala tuhoutuu
            Destroy(gameObject);

            // Lis�tt�v�t pisteet
            int value = 5;
            GameLogic.score += value;

            // Tulostetaan pisteet konsoliin
            Debug.Log("Pisteet: " + GameLogic.score);
        }
    }
}