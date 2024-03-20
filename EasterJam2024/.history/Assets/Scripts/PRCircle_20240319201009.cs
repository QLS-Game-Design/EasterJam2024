using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PRCircle : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Circle;
    public string targetTag = "PopRock";

    private GameObject enemy;
    private EnemyController enemyController;
    void Start()
    {
        enemy= GameObject.FindGameObjectWithTag("Enemy");
        enemyController = enemy.GetComponent<EnemyController>();
    }

    // Update is called once per frame
    void Update()
    {

        if (enemyController.areaDamage) {

        }
        
    }
}
