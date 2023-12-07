using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class KalaSpawner : MonoBehaviour
{
    public GameObject kalanPrefab; // Kalan esiprefab, joka spawnerataan
    private float spawnIntervalMin = 4f; // Lyhin aika tukin spawnerauksen välillä
    private float spawnIntervalMax = 5f; // Pisin aika tukin spawnerauksen välillä

    private float timeSinceLastSpawn; // Aika viimeisestä spawnerauksesta
    private float currentSpawnInterval; // Nykyinen aika seuraavaan spawneraukseen

    private float milestone = 50f;

    void Start()
    {
        currentSpawnInterval = Random.Range(spawnIntervalMin, spawnIntervalMax); // Aseta satunnainen aika seuraavalle spawneraukselle
    }

    void Update()
    {
        if (milestone < GameLogic.score && milestone < 400)
        {
            spawnIntervalMin = spawnIntervalMin * 0.75f;
            spawnIntervalMax = spawnIntervalMax * 0.75f;
            milestone += 50;
        }


        timeSinceLastSpawn += Time.deltaTime; // Laske kulunut aika viimeisestä spawnerauksesta

        if (timeSinceLastSpawn >= currentSpawnInterval)
        {
            SpawnKala(); // Spawnataan kala

            SetNextSpawnTime(); // Aseta aika seuraavalle spawneraukselle
        }
    }

    void SetNextSpawnTime()
    {
        currentSpawnInterval = Random.Range(spawnIntervalMin, spawnIntervalMax); // Aseta satunnainen aika seuraavalle spawneraukselle
        timeSinceLastSpawn = 0f; // Nollaa aika viimeisestä spawnerauksesta
    }

    void SpawnKala()
    {
        // Spawnataan tukki ja asetetaan sen sijainti satunnaisesti kolmesta kaistasta (A, B tai C) 
        string kaista = RandomKaista(); // Valitaan satunnainen kaista

        if (kaista == SpawnManager.kaistaKirjain)
        {
            kaista = OtettuSijainti(kaista);
            Vector3 spawnSijainti = CalculateSpawnPosition(kaista); // Lasketaan spawnerin sijainti valitun kaistan perusteella

            GameObject fish = Instantiate(kalanPrefab, spawnSijainti, Quaternion.Euler(0f, 180f, 0f)); // Luo Kala spawnerin sijaintiin
            SpawnManager.kaistaKirjain = kaista;
        }
        else
        {
            Vector3 spawnSijainti = CalculateSpawnPosition(kaista); // Lasketaan spawnerin sijainti valitun kaistan perusteella

            GameObject fish = Instantiate(kalanPrefab, spawnSijainti, Quaternion.Euler(0f, 180f, 0f)); // Luo Kala spawnerin sijaintiin
            SpawnManager.kaistaKirjain = kaista;
        }
    }

    string OtettuSijainti(string kaista)
    {
        return kaista == "A" ? "B" : kaista == "B" ? "C" : kaista == "C" ? "A" : "A";
    }

    string RandomKaista()
    {
        int kaistaNumero = Random.Range(1, 3); // Valitse satunnainen kaista numerona (1, 2 tai 3)
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

