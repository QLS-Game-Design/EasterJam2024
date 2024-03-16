using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float health;
    public float speed;
    public float origSpeed;
    public GameObject player;
    Rigidbody2D rigidbody2D;
    Vector2 moveDirection;
    float time;
    bool slowed;
    float slowAmount = 3.0f;

    float stunAmount = 2.0f;
    bool stunned;
    // Start is called before the first frame update
    void Start()
    {
        health = 10;
        origSpeed = 3.0f;
        speed = origSpeed;
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
{
    if (slowed || stunned) {
        time += Time.deltaTime;
    }
    if (slowed && time >= slowAmount) {
        time = 0;
        slowed = false;
        speed = origSpeed;
    }
    if (stunned && time >= stunAmount) {
        time = 0;
        stunned = false;
        speed = origSpeed;
    }

    Vector3 direction = (player.transform.position - transform.position).normalized;
    float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
    rigidbody2D.rotation = angle + 90;
    moveDirection = direction;
    rigidbody2D.velocity = new Vector2(moveDirection.x, moveDirection.y) * speed;
}

    void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("HardCandy")) {
            health -= 5;
        } else if (collision.gameObject.CompareTag("SoftCandy")) {
            health -= 2;
            speed -= 2.0f;
            slowed = true;
        } else if (collision.gameObject.CompareTag("Gum")) {
            health -= 1;
            speed = 0;
            stunned = true;
        }
    }
}
