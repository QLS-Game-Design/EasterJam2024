using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthBar : MonoBehaviour
{
    PlayerController player;
    private UnityEngine.UI.Image Healthbar;
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponentInParent<PlayerController>();
        Healthbar = GetComponent<UnityEngine.UI.Image>();
    }

    // Update is called once per frame
    void Update()
    {
        Healthbar.fillAmount = player.currHealth / player.maxHealth;
    }
}
