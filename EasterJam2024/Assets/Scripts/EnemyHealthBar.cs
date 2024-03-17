using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthBar : MonoBehaviour
{
    EnemyController enemy;
    private UnityEngine.UI.Image Healthbar;
    // Start is called before the first frame update
    void Start()
    {
        enemy = GetComponentInParent<EnemyController>();
        Healthbar = GetComponent<UnityEngine.UI.Image>();
    }

    // Update is called once per frame
    void Update()
    {
        Healthbar.fillAmount = enemy.currHealth / enemy.maxHealth;
    }
}
