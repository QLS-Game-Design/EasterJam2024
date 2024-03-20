using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using Pathfinding;
using UnityEngine.SocialPlatforms.Impl;

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
    public bool areaDamage = false;
    
    public ParticleSystem rockParticles; // Reference to the Particle System
    public Vector3 spawnPosition;
    float stunAmount = 2.0f;
    bool stunned;
    public Slider progressBar;
    public float progressIncrement = 1.0f;
    public AIPath path;
    public delegate void AreaDamageEvent(EnemyController enemyController);
    public static event AreaDamageEvent OnAreaDamage;
    public GameObject upgrade;
    private PlayerController playerController;



    // Start is called before the first frame update
    void Start()
    {
        progressBar = GameObject.Find("ProgressBar").GetComponent<Slider>();
        maxHealth = 10;
        currHealth = maxHealth;
        origSpeed = 3.0f;
        speed = origSpeed;
        rigidbody2D = GetComponent<Rigidbody2D>();
        progressBar.maxValue = 3;
        upgrade.SetActive(false);
        path = GetComponent<AIPath>();
        player = GameObject.FindGameObjectWithTag("Player");
        playerController = player.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        path.maxSpeed = speed;

        if (currHealth <= 0)
        {
            Destroy(gameObject);
            IncrementProgressBar();
            player
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

        // Vector3 direction = (player.transform.position - transform.position).normalized;
        // float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        // // rigidbody2D.rotation = angle + 90;
        // moveDirection = direction;
        // rigidbody2D.velocity = new Vector2(moveDirection.x, moveDirection.y) * speed;
    
        // float movingDirection = rigidbody2D.velocity.x;
        if (path.desiredVelocity.x < 0f && isFacingRight)
        {
            Flip();
        }
        else if (path.desiredVelocity.x > 0 && !isFacingRight)
        {
            Flip();
        }
    }
     void UpdateProgressBar(float progress)
    {
        if (progressBar != null)
        {
            progressBar.value = progress;
        }
    }
        void IncrementProgressBar()
    {
        if (progressBar != null)
        {
            progressBar.value += progressIncrement; 
        }
        if (progressBar.value == progressBar.maxValue)
        {
            upgrade.SetActive(true);
            Debug.Log("MAX PROGRESS REACHED!");
        }
    }
    void Flip()
    {
        isFacingRight = !isFacingRight;
        GetComponent<SpriteRenderer>().flipX = !isFacingRight;
    }

     void SpawnParticles(Vector3 position)
    {
        // Set the position of the Particle System to the trigger enter position
        rockParticles.transform.position = position;

        // Emit particles
        rockParticles.Emit(15);
        Debug.Log("particles");
    }
    private void DoAreaDamage()
    {
        areaDamage = true;
        // Other logic...
        
        // Raise the event
        if (OnAreaDamage != null)
        {
            OnAreaDamage(this);
        }
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
        else if (other.CompareTag("PopRock")) {
            currHealth -= 2;
            spawnPosition = other.transform.position; // Get the position of the trigger enter event
            SpawnParticles(spawnPosition);
            DoAreaDamage();
            Destroy(other.gameObject);
        } 
        else if (other.CompareTag("CandyCorn")) {
            currHealth -= 4;
            Debug.Log("Hit with Hard Candy");
            Destroy(other.gameObject, 1.5f);
        }
        else if (other.CompareTag("PRCircle")) {
            currHealth -= 2;
        }
    }
}
