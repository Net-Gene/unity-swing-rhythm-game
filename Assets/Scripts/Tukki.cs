using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Random = UnityEngine.Random;

public class Tukki : MonoBehaviour
{
    public float initialSpawnIntervalMin = 6f;
    public float initialSpawnIntervalMax = 5f;
    public float despawnKorkeus = -10f;
    public int kaista; // 0, 1, 2 edustaa kolmea kaistaa
    public GameObject pelaaja;
    // public GameManager pelinJohtaja;
    private float nopeus = 5f;
    

    private float currentSpawnIntervalMin;
    private float currentSpawnIntervalMax;
    private float timeSinceLastSpawn;
    private float currentSpawnTime;

    void Start()
    {
        currentSpawnIntervalMin = initialSpawnIntervalMin;
        currentSpawnIntervalMax = initialSpawnIntervalMax;

        pelaaja = GameObject.Find("Pelaaja"); // Pelaaja voidaan paikata myos joen lopussa olevalla hitboksilla
        // pelinJohtaja = GameObject.Find("GameManager").GetComponent<GameManager>(); // GameManager on pelin tilan hallintaan
    }

    void Update()
    {
        timeSinceLastSpawn += Time.deltaTime;

        if (timeSinceLastSpawn >= currentSpawnTime)
        {
            SpawnTukki();
            SetNextSpawnTime();
        }

        // Liikuttaa tukkia eteenpain
        transform.Translate(Vector3.back * nopeus * Time.deltaTime);

        // Tarkista, onko tukki osunut pelaajan hitboxiin
        if (transform.position.z <= pelaaja.transform.position.z)
        {
            if (transform.position.x == pelaaja.transform.position.x)
            {
                // Pelaaja ei lyönyt tukkia oikeaan aikaan, pelaaja haviaa
                print("Havisit");
                // pelinJohtaja.PelaajaHavisi();
            }
            Destroy(gameObject); // Tukki despawnataan, kun se osuu pelaajaan
        }

        // Tarkista, onko tukki liian alhaalla ja despawnataan
        if (transform.position.z <= despawnKorkeus)
        {
            Destroy(gameObject);
        }
    }

    void SetNextSpawnTime()
    {
        currentSpawnTime = Random.Range(currentSpawnIntervalMin, currentSpawnIntervalMax);
        timeSinceLastSpawn = 0f;

        // Paivita spawnausnopeuksia
        currentSpawnIntervalMin -= 1f; // Vahenna minimiaikaa
        currentSpawnIntervalMax -= 1f; // Vahenna maksimiaikaa
        currentSpawnIntervalMin = Mathf.Max(currentSpawnIntervalMin, 3f); // Esta liian nopea spawnaus
        currentSpawnIntervalMax = Mathf.Max(currentSpawnIntervalMax, 2f);
    }

    void SpawnTukki()
    {
        // Aseta tukin sijainti ja kaista satunnaisesti
        kaista = Random.Range(0, 3);
        Vector3 spawnSijainti = new Vector3(kaista - 1, transform.position.y, transform.position.z);
        transform.position = spawnSijainti;
    }

}
