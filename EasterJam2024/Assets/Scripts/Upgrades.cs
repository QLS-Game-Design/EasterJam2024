using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrades : MonoBehaviour
{
    public PlayerController player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (EnemyController.xp >= EnemyController.threshold) {
            EnemyController.xp = 0;
            EnemyController.threshold += 1;
            Transform thisChild = transform.GetChild(0);
            thisChild.gameObject.SetActive(true);
            for (int i = 0; i < thisChild.childCount; i++) {
                thisChild.GetChild(i).gameObject.SetActive(true);
                thisChild.GetChild(i).GetChild(0).gameObject.SetActive(true);
            }
        }
    }

    public void DamageUpgrade() {
        EnemyController.hardCandyDamage += 1.0f;
        EnemyController.softCandyDamage += 0.5f;
        EnemyController.gumDamage += 0.5f;
        EnemyController.popRockDamage += 1.0f;
        EnemyController.candyCornDamage += 1.0f;
        Debug.Log("Damage Upgrade");
        Finish();
    }

    public void ArrowsUpgrade() {
        EnemyController.hardCandyDamage += 0.5f;
        EnemyController.candyCornDamage += 0.5f;
        EnemyController.prAreaDamage += 1.0f;
        EnemyController.slowAmount += 0.5f;
        EnemyController.stunAmount += 0.5f;
        Debug.Log("Arrows Upgrade");
        Finish();
    }

    public void HealthUpgrade() {
        player.maxHealth += 5;
        player.currHealth = player.maxHealth;
        Debug.Log("Health Upgrade");
        Finish();
    }

    void Finish() {
        for (int i = 0; i < transform.childCount; i++) {
            transform.GetChild(i).gameObject.SetActive(false);
            transform.GetChild(i).GetChild(0).gameObject.SetActive(false);
        }
    }
}
