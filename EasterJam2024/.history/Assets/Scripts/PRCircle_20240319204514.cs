using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PRCircle : MonoBehaviour
{
    public GameObject Circle;
    public string targetTag = "PopRock";

    private void OnEnable()
    {
        EnemyController.OnAreaDamage += HandleAreaDamage;
    }

    private void OnDisable()
    {
        EnemyController.OnAreaDamage -= HandleAreaDamage;
    }

    private void HandleAreaDamage(EnemyController enemyController)
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