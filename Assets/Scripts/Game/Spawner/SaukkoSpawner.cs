using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaukkoSpawner : MonoBehaviour
{
    public GameObject saukonPrefab; // Kalan esiprefab, joka spawnerataan
    private float spawnIntervalMin = 4f; //20f; // Lyhin aika tukin spawnerauksen välillä
    private float spawnIntervalMax = 5f; //40f; // Pisin aika tukin spawnerauksen välillä

    private float timeSinceLastSpawn = 0f; // Aika viimeisestä spawnerauksesta
    private float currentSpawnInterval = 99999999; // Nykyinen aika seuraavaan spawneraukseen

    private bool f = false;

    void Start()
    {
        int chance = Random.Range(1, 100);
        if(chance <= 10)
        {
            currentSpawnInterval = Random.Range(spawnIntervalMin, spawnIntervalMax); // Aseta satunnainen aika seuraavalle spawneraukselle
        }
    }

    void Update()
    {
        if(f == false)
        {
            timeSinceLastSpawn += Time.deltaTime; // Laske kulunut aika viimeisestä spawnerauksesta

            if (timeSinceLastSpawn >= currentSpawnInterval)
            {
                SpawnSaukko(); // Spawnataan kala
                SetNextSpawnTime();
                f = true;
            }
        }
        
    }

    void SetNextSpawnTime()
    {
        currentSpawnInterval = Random.Range(spawnIntervalMin, spawnIntervalMax); // Aseta satunnainen aika seuraavalle spawneraukselle
        timeSinceLastSpawn = 0f; // Nollaa aika viimeisestä spawnerauksesta
    }

    void SpawnSaukko()
    {
        // Spawnataan tukki ja asetetaan sen sijainti satunnaisesti kolmesta kaistasta (A, B tai C) 
        string kaista = RandomKaista(); // Valitaan satunnainen kaista
        Vector3 spawnSijainti = CalculateSpawnPosition(kaista); // Lasketaan spawnerin sijainti valitun kaistan perusteella 

        GameObject goldenSaukko = Instantiate(saukonPrefab, spawnSijainti, Quaternion.Euler(-90f, 180f, 0f)); // Luo Kala spawnerin sijaintiin
    }

    string RandomKaista()
    {
        int kaistaNumero = Random.Range(1, 4); // Valitse satunnainen kaista numerona (1, 2 tai 3)
        return kaistaNumero == 1 ? "A" : (kaistaNumero == 2 ? "B" : "C"); // Muunna numeron vastaavaksi kaistaksi (A, B tai C)
    }

    Vector3 CalculateSpawnPosition(string kaista)
    {
        Vector3 spawnSijainti = Vector3.zero;

        if (kaista == "A")
        {
            spawnSijainti = new Vector3(-3f, -0.15f, 10f); // Aseta sijainti kaistan A mukaan
        }
        else if (kaista == "B")
        {
            spawnSijainti = new Vector3(0f, -0.15f, 10f); // Aseta sijainti kaistan B mukaan
        }
        else if (kaista == "C")
        {
            spawnSijainti = new Vector3(3f, -0.15f, 10f); // Aseta sijainti kaistan C mukaan
        }

        return spawnSijainti; // Palauta laskettu sijainti
    }
}

