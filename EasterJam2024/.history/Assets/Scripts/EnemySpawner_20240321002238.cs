using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] children;
    public float initialSpawnInterval = 1.5f;
    public float minSpawnInterval = 0.5f;
    public float decreaseRate = 0.1f; // The rate at which the spawn interval decreases
    public GameObject enemy;

    float time;

    void Start()
    {
        children = GetComponentsInChildren<GameObject>();
        StartCoroutine(DecreaseSpawnInterval());
    }

    void Update()
    {
        time += Time.deltaTime;
        if (time >= spawnInterval)
        {
            GameObject place = children[Random.Range(0, children.Length)];
            GameObject newEnemy = Instantiate(enemy);
            newEnemy.transform.position = place.transform.position;
            time = 0;
        }
    }

    IEnumerator DecreaseSpawnInterval()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f); // Wait for 1 second before decreasing the spawn interval
            spawnInterval = Mathf.Max(spawnInterval - decreaseRate, minSpawnInterval);
        }
    }
}
