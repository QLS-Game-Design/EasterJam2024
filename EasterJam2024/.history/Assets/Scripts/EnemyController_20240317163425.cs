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
    bool isFacingRight = true;

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
    // rigidbody2D.rotation = angle + 90;
    moveDirection = direction;
    rigidbody2D.velocity = new Vector2(moveDirection.x, moveDirection.y) * speed;
   
    float movingDirection = rigidbody2D.velocity.x;
    if (movingDirection > 0 && !isFacingRight)
        {
            Flip();
        }
        else if (movingDirection < 0 && isFacingRight)
        {
            Flip();
        }
    }
    void Flip()
    {
        // Flip the enemy by reversing its scale along the x-axis
        isFacingRight = !isFacingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("HardCandy")) {
            currHealth -= 5;
            Debug.Log("Hit with Hard Candy");
            Destroy(other.gameObject);
        } 
        else if (other.CompareTag("SoftCandy")) {
            currHealth -= 4;
            speed -= 2.0f;
            slowed = true;
            Debug.Log("Hit with Soft Candy");
            Destroy(other.gameObject);
        } 
        else if (other.CompareTag("Gum")) {
            currHealth -= 2;
            speed = 0;  
            stunned = true;
            Debug.Log("Hit with Gum");
            Destroy(other.gameObject);
        }
}
}
