using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrades : MonoBehaviour
{
    public PlayerController player;
    public static bool stop;
    public static bool unstopped;
    float time;
    // Start is called before the first frame update
    void Start()
    {
        stop = false;
        unstopped = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (EnemyController.xp >= EnemyController.threshold) {
            stop = true;
            EnemyController.xp = 0;
            EnemyController.threshold += 1;
            Transform thisChild = transform.GetChild(0);
            thisChild.gameObject.SetActive(true);
            for (int i = 0; i < thisChild.childCount; i++) {
                thisChild.GetChild(i).gameObject.SetActive(true);
                thisChild.GetChild(i).GetChild(0).gameObject.SetActive(true);
            }
        }
        if (unstopped && time < 0.1f) {
            time += Time.deltaTime;
        } else if (unstopped && time >= 0.1f) {
            unstopped = false;
            time = 0;
        }
    }

    public void DamageUpgrade() {
        EnemyController.hardCandyDamage += 0.1f;
        EnemyController.softCandyDamage += 0.1f;
        EnemyController.gumDamage += 0.1f;
        EnemyController.popRockDamage += 0.1f;
        EnemyController.candyCornDamage += 0.1f;
        Debug.Log("Damage Upgrade");
        Finish();
    }

    public void ArrowsUpgrade() {
        // EnemyController.hardCandyDamage += 0.1f;
        // EnemyController.candyCornDamage += 0.1f;
        EnemyController.prAreaDamage += 0.1f;
        EnemyController.slowAmount += 0.1f;
        EnemyController.stunAmount += 0.1f;
        Debug.Log("Arrows Upgrade");
        Finish();
    }

    public void HealthUpgrade() {
        player.maxHealth += 3;
        player.currHealth +=;
        Debug.Log("Health Upgrade");
        Finish();
    }

    void Finish() {
        stop = false;
        unstopped = true;
        for (int i = 0; i < transform.childCount; i++) {
            transform.GetChild(i).gameObject.SetActive(false);
            transform.GetChild(i).GetChild(0).gameObject.SetActive(false);
        }
    }
}
