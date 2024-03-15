using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float health;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        health = 10;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2d(Collision2D collision) {
        if (collision.gameObject.CompareTag("HardCandy")) {
            health -= 5;
        } else if (collision.gameObject.CompareTag("SoftCandy")) {

        }
    }
}
