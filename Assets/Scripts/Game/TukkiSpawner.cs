using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class TukkiSpawner : MonoBehaviour
{
    public GameObject tukinPrefab;
    public float spawnIntervalMin = 5f;
    public float spawnIntervalMax = 6f;

    private float timeSinceLastSpawn;
    private float currentSpawnInterval;

    void Start()
    {
        currentSpawnInterval = Random.Range(spawnIntervalMin, spawnIntervalMax);
    }

    void Update()
    {
        timeSinceLastSpawn += Time.deltaTime;

        if (timeSinceLastSpawn >= currentSpawnInterval)
        {
            SpawnTukki();
            SetNextSpawnTime();
        }
    }

    void SetNextSpawnTime()
    {
        currentSpawnInterval = Random.Range(spawnIntervalMin, spawnIntervalMax);
        timeSinceLastSpawn = 0f;
    }

    void SpawnTukki()
    {
        // Spawnataan tukki ja asetetaan sen sijainti satunnaisesti kolmesta kaistasta (A, B tai C) 
        string kaista = RandomKaista(); // Valitaan kaista
        Vector3 spawnSijainti = CalculateSpawnPosition(kaista); // Lasketaan spawni kohta kaistan mukaan 
        GameObject tukki = Instantiate(tukinPrefab, spawnSijainti, Quaternion.identity);
    }

    string RandomKaista()
    {
        int kaistaNumero = Random.Range(1, 4);
        return kaistaNumero == 1 ? "A" : (kaistaNumero == 2 ? "B" : "C");
    }

    Vector3 CalculateSpawnPosition(string kaista)
    {
        Vector3 spawnSijainti = Vector3.zero;

        if (kaista == "A")
        {
            spawnSijainti = new Vector3(-3f, 0f, 10f);
        }
        else if (kaista == "B")
        {
            spawnSijainti = new Vector3(0f, 0f, 10f);
        }

        else if (kaista == "C")
        {
            spawnSijainti = new Vector3(3f, 0f, 10f);
        }

        return spawnSijainti;
    }
}

