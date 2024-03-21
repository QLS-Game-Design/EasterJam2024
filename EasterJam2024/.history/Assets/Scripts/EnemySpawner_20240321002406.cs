using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] children;
    public float spawnInterval;
      public float initialSpawnInterval = 1.5f;
    public float minSpawnInterval = 0.5f;
    public float decreaseRate = 0.1f;
    float time;
    public GameObject enemy;
    // Start is called before the first frame update
    void Start()
    {
        children = GetComponentsInChildren<GameObject>();
        spawnInterval = 1.5f;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (time >= spawnInterval) {
            GameObject place = children[Random.Range(0, children.Length)];
            GameObject Enemy = Instantiate(enemy);
            Enemy.transform.position = place.transform.position;
            time = 0;
        }
        if (spawnInterval < 0.5f) {
            spawnInterval = 0.5f;
        }
    }

    IEnumerator DecreaseSpawnInterval()
    {
        while (spawnInterval > 0.5f)
        {
            yield return new WaitForSeconds(1f); // Wait for 1 second before decreasing the spawn interval
            spawnInterval = Mathf.Max(spawnInterval - decreaseRate, minSpawnInterval);
        }
    }
}
