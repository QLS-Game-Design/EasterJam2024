using System.Collections.Generic;
using System.Data.Common;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public Vector2 playerInput;
    Rigidbody2D rb;
    public float moveSpeed;
    public float inputHorizontal;
    public bool facingRight = true;
    public bool isFlipped = true;
    public float orientation = 1;
    public float score;
    private Animator anim;
    public float maxHealth;
    public float currHealth;
    public GameOverScreen gameOverScreen;
    public GameObject Player;
    public TextMeshProUGUI scoreText;
    public AudioClip[] soundClips; 
    private AudioSource audioSource;

    public bool isDead = false;
    

    // Start is called before the first frame update
    void Start()
    {
        isDead = false;
        audioSource = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        //anim = GetComponent<Animator>();
        maxHealth = 10;
        currHealth = maxHealth;
        score = 0;

        EnemyController.origSpeed = 3.0f;

        EnemyController.hardCandyDamage = 2.0f;
        EnemyController.softCandyDamage = 1.0f;
        EnemyController.gumDamage = 0.5f;
        EnemyController.popRockDamage = 1.0f;
        EnemyController.candyCornDamage = 1.5f;
        EnemyController.prAreaDamage = 0.5f;
        EnemyController.slowAmount = 1.0f;
        EnemyController.stunAmount = 1.0f;

        EnemyController.xp = 0;
        EnemyController.threshold = 2;
    }

    // Update is called once per frame
    void Update()
    {
        // Debug.Log(Input.GetAxisRaw("Horizontal"));
        if (!Upgrades.stop) {
            playerInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            rb.velocity = playerInput.normalized * moveSpeed;

            inputHorizontal = Input.GetAxisRaw("Horizontal");
            if (inputHorizontal > 0 && !facingRight) {
                flip();
                
            } else if (inputHorizontal < 0 && facingRight) {
                flip();
                
            } 

            if (Input.GetAxis("Horizontal") == 0 && Input.GetAxis("Vertical") == 0) {
                anim.SetBool("isRunning", false);
            } else if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) == 1 || Mathf.Abs(Input.GetAxisRaw("Vertical")) == 1){
                anim.SetBool("isRunning", true);
            }
        }
        
        if (currHealth <= 0) {
            die();
        }
        // Debug.Log("score is " + score.ToString());
        scoreText.text = score.ToString() + " POINTS";

        
    }

    public void enemyDie() {
        audioSource.clip = soundClips[0];
        Debug.Log("playsound");
        // Play the sound
        audioSource.Play();
 
    }
    void die() {
        isDead = true;
        Destroy(Player);
        gameOverScreen.Setup((int)score);
    }
    void flip() {
        Vector3 currentScale = gameObject.transform.localScale;
        currentScale.x *= -1;
        gameObject.transform.localScale = currentScale;
        orientation *= -1;
        facingRight = !facingRight;
        isFlipped = !isFlipped;
    }

    
    void OnCollisionStay2D(Collision2D collision) {
    if (collision.gameObject.CompareTag("Enemy")) {
        // Apply damage to the player as long as they are in collision with the enemy
        ApplyDamage();
    }
}

void ApplyDamage() {
    // Apply damage at regular intervals while in collision with the enemy
    currHealth -= 3;
    audioSource.clip = soundClips[1];
    audioSource.Play();
    Debug.Log("Attacked");

    // Schedule the next damage application after a short delay
    Invoke("ApplyDamage", 2.0f); // Adjust the delay as needed
}   
    void IncrementScore(int amt) {
        score += amt;
    }
}
