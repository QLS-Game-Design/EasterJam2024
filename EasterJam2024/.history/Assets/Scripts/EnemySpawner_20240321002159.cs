using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] children;
    public float initialSpawnInterval = 1.5f;
    public float minSpawnInterval = 0.5f;
    public float decreaseRate = 0.1f; // Rate at which spawn interval decreases
    public float decreaseInterval = 10f; // Interval at which the spawn interval decreases
    float timeSinceLastDecrease = 0f;
    float time;

    public GameObject enemy;

    void Start()
    {
        children = GetComponentsInChildren<GameObject>();
        spawnInterval = initialSpawnInterval;
    }

    void Update()
    {
        time += Time.deltaTime;

        // Check if it's time to decrease the spawn interval
        if (timeSinceLastDecrease >= decreaseInterval)
        {
            DecreaseSpawnInterval();
            timeSinceLastDecrease = 0f;
        }
        else
        {
            timeSinceLastDecrease += Time.deltaTime;
        }

        if (time >= spawnInterval)
        {
            GameObject place = children[Random.Range(0, children.Length)];
            GameObject Enemy = Instantiate(enemy);
            Enemy.transform.position = place.transform.position;
            time = 0;
        }
    }

    void DecreaseSpawnInterval()
    {
        spawnInterval = Mathf.Max(minSpawnInterval, spawnInterval - decreaseRate);
    }

    public void DecreaseInterval()
    {
        spawnInterval -= 0.1f;
    }
}