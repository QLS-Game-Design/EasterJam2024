using UnityEngine;
using UnityEngine.UI;
using Pathfinding;
using NUnit.Framework;
using System.Xml;

public class EnemyController : MonoBehaviour
{
    public float currHealth;
    public float maxHealth;
    public float speed;
    public static float origSpeed = 3.0f;
    public GameObject player;
    // Rigidbody2D rigidbody2D;
    Vector2 moveDirection;
    float time;
    bool slowed;
    
    bool isFacingRight = true;
    public bool areaDamage = false;
    
    public ParticleSystem rockParticles; // Reference to the Particle System
    public Vector3 spawnPosition;
    
    bool stunned;
    // public Slider progressBar;
    public float progressIncrement = 1.0f;
    public AIPath path;
    public delegate void AreaDamageEvent(EnemyController enemyController);
    public static event AreaDamageEvent OnAreaDamage;
    // public GameObject upgrade;
    // private PlayerController playerController;

    public ParticleSystem deathParticles;


    public AIDestinationSetter destinationSetter;
    public EnemySpawner spawner;
    public UpgradeOn upgrade;

    // damages
    public static float hardCandyDamage;
    public static float softCandyDamage;
    public static float gumDamage;
    public static float popRockDamage;
    public static float candyCornDamage;
    public static float prAreaDamage;
    public static float slowAmount;
    public static float stunAmount;

    public static int level;
    public static int xp;
    public static int threshold;
    public AudioClip[] soundClips; // Array to hold multiple sound clips
    private AudioSource audioSource;
    private PlayerController playerController

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        // progressBar = GameObject.Find("ProgressBar").GetComponent<Slider>();
        maxHealth = 10;
        
        currHealth = maxHealth;
        speed = origSpeed;
        // rigidbody2D = GetComponent<Rigidbody2D>();
        // progressBar.maxValue = 3;
        // upgrade.SetActive(false);
        path = GetComponent<AIPath>();
        destinationSetter = GetComponent<AIDestinationSetter>();

        Debug.Log("spawned");
        
        player = GameObject.FindGameObjectWithTag("Player");
        if (player != null) {
            Debug.Log("yay");
        }
        // playerController = player.GetComponent<PlayerController>();
        if (player.transform != null) {
            destinationSetter.target = player.transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        path.maxSpeed = speed;
        if (UpgradeOn.stop) {
            speed = 0;
        }
        if (UpgradeOn.unstop) {
            speed = origSpeed;
        }

        if (currHealth <= 0)
        {
            player
            Destroy(gameObject);
            // IncrementProgressBar();
            spawner.spawnInterval -= 0.05f;
            player.BroadcastMessage("IncrementScore", 5);
            xp++;
            origSpeed = 3.0f + level*0.05f;
            EmitDeathParticles();
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

        if (xp >= threshold) {
            level++;
            upgrade.Upgrade();
        }
    }

    void EmitDeathParticles()
    {
        ParticleSystem clonedDeathParticles = Instantiate(deathParticles, transform.position, Quaternion.identity);
        Destroy(clonedDeathParticles,1);
    }
    // void UpdateProgressBar(float progress)
    // {
    //     if (progressBar != null)
    //     {
    //         progressBar.value = progress;
    //     }
    // }
    // void IncrementProgressBar()
    // {
    //     if (progressBar != null)
    //     {
    //         progressBar.value += progressIncrement; 
    //     }
    //     if (progressBar.value == progressBar.maxValue)
    //     {
    //         // upgrade.SetActive(true);
    //         Debug.Log("MAX PROGRESS REACHED!");
    //     }
    // }
    void Flip()
    {
        isFacingRight = !isFacingRight;
        GetComponent<SpriteRenderer>().flipX = !isFacingRight;
    }

    void SpawnParticles(Vector3 position)
    {
        // Set the position of the Particle System to the trigger enter position
        ParticleSystem clonedParticles = Instantiate(rockParticles, transform.position, Quaternion.identity);

        // Emit particles
        clonedParticles.Emit(15);

        Destroy(clonedParticles, 1f);
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

    public void PlaySound(int index)
    {
        if (index >= 0 && index < soundClips.Length)
        {
            // Set the AudioClip to play
            audioSource.clip = soundClips[index];
            Debug.Log("playsound");
            // Play the sound
            audioSource.Play();
        }
        else
        {
            Debug.LogWarning("Invalid sound index");
        }
    }
    void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("HardCandy")) {
            PlaySound(0);
            
            currHealth -= hardCandyDamage;
            Debug.Log("Hit with Hard Candy");
            Destroy(other.gameObject);
        } 
        else if (other.CompareTag("SoftCandy")) {
            PlaySound(0);
            
            currHealth -= softCandyDamage;
            speed -= 2.0f;
            slowed = true;
            Debug.Log("Hit with Soft Candy");
            Destroy(other.gameObject);
        } 
        else if (other.CompareTag("Gum")) {
            PlaySound(2);
            currHealth -= gumDamage;
            speed = 0;  
            stunned = true;
            Debug.Log("Hit with Gum");
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("PopRock")) {
            PlaySound(1);
            spawnPosition = other.transform.position; // Get the position of the trigger enter event
            currHealth -= popRockDamage;
            spawnPosition = other.transform.position; // Get the position of the trigger enter event\
            Debug.Log("1");
            SpawnParticles(spawnPosition);
            Debug.Log("2");
            DoAreaDamage();
            Debug.Log("3");
            Destroy(other.gameObject);
        } 
        else if (other.CompareTag("CandyCorn")) {
            PlaySound(0);
            currHealth -= candyCornDamage;
            Debug.Log("Hit with Hard Candy");
            Destroy(other.gameObject, 1.5f);
        }
        else if (other.CompareTag("PRCircle")) {
            currHealth -= prAreaDamage;
        }
    }
}
