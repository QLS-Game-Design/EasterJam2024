using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PRCircle : MonoBehaviour
{
    public GameObject Circle;
    public string targetTag = "PopRock";

    private EnemyController[] enemyControllers;

    void Start()
    {
        enemyControllers = FindObjectsOfType<EnemyController>();
    }

    void Update()
    {
        foreach (EnemyController enemyController in enemyControllers)
        {
            if (enemyController.areaDamage)
            {
                Debug.Log("clone");

                GameObject targetObject = GameObject.FindGameObjectWithTag(targetTag);

                if (targetObject != null)
                {
                    GameObject clone = Instantiate(Circle);
                    clone.transform.position = enemyController.spawnPosition;
                    Destroy(clone, 1.0f);
                    enemyController.areaDamage = false;
                }
                else
                {
                    Debug.LogError("No object found with tag: " + targetTag);
                }
            }
        }
    }
}
