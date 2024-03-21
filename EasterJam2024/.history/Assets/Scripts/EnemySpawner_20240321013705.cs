using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] children;
    public GameObject enemyPrefab;
    public float spawnInterval;
    public float initialSpawnInterval = f;
    public float minSpawnInterval = 0.5f;
    public float decreaseRate = 0.1f;
    private float timeElapsed;

    void Start()
    {
        children = GetComponentsInChildren<Transform>().Select(t => t.gameObject).ToArray();
        spawnInterval = initialSpawnInterval;
        StartCoroutine(DecreaseSpawnInterval());
    }

    void Update()
    {
        timeElapsed += Time.deltaTime;
        if (timeElapsed >= spawnInterval)
        {
            GameObject place = children[Random.Range(0, children.Length)];
            GameObject spawnedEnemy = Instantiate(enemyPrefab, place.transform.position, Quaternion.identity);
            timeElapsed = 0;
        }
    }

    IEnumerator DecreaseSpawnInterval()
    {
        while (spawnInterval > minSpawnInterval)
        {
            yield return new WaitForSeconds(2f);
            spawnInterval = Mathf.Max(spawnInterval - decreaseRate, minSpawnInterval);
        }
    }
}