using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] children;
    public float spawnInterval;
    float time;
    public GameObject enemy;
    // Start is called before the first frame update
    void Start()
    {
        children = GetComponentsInChildren<GameObject>();
        spawnInterval = 4.0f;
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
}
