using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeController : MonoBehaviour
{
    PlayerController player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void DamageUpgrade() {
        EnemyController.hardCandyDamage += 1.0f;
        EnemyController.softCandyDamage += 0.5f;
        EnemyController.gumDamage += 0.5f;
        EnemyController.popRockDamage += 1.0f;
        EnemyController.candyCornDamage += 1.0f;
    }

    void ArrowsUpgrade() {
        EnemyController.hardCandyDamage += 0.5f;
        EnemyController.candyCornDamage += 0.5f;
        EnemyController.prAreaDamage += 1.0f;
        EnemyController.slowAmount += 0.5f;
        EnemyController.stunAmount += 0.5f;
    }

    void HealthUpgrade() {
        player.maxHealth += 5;
        player.currHealth = player.maxHealth;
    }
}
