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
        // Luo tukin ja aseta sen sijainti spawnaajan sijainnin perusteella 

        Vector3 spawnSijainti = transform.position;
        GameObject tukki = Instantiate(tukinPrefab, spawnSijainti, Quaternion.identity);
    }
}

