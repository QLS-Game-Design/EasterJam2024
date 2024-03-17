using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float currHealth;
    public float maxHealth;
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
        maxHealth = 10;
        currHealth = maxHealth;
        origSpeed = 3.0f;
        speed = origSpeed;
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
   void Update()
{
    if (currHealth <= 0)
    {
        Destroy(gameObject);
    }

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

    if (speed < 0) {
        speed = 0;
    }

    Vector3 direction = (player.transform.position - transform.position).normalized;
    float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
    rigidbody2D.rotation = angle + 90;
    moveDirection = direction;
    rigidbody2D.velocity = new Vector2(moveDirection.x, moveDirection.y) * speed;
}

    void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("HardCandy")) {
            currHealth -= 5;
            Debug.Log("Hit with Hard Candy");
            Destroy(collision.gameObject);
        } else if (collision.gameObject.CompareTag("SoftCandy")) {
            currHealth -= 4;
            speed -= 2.0f;
            slowed = true;
            Debug.Log("Hit with Soft Candy");
            Destroy(collision.gameObject);
        } else if (collision.gameObject.CompareTag("Gum")) {
            currHealth -= 2;
            speed = 0;
            stunned = true;
            Debug.Log("Hit with Gum");
            Destroy(collision.gameObject);
        }
    }
}
