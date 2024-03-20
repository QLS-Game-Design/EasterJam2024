using UnityEngine;
using UnityEngine.UI;
using Pathfinding;

public class EnemyController : MonoBehaviour
{
    public float currHealth;
    public float maxHealth;
    public float speed;
    public float origSpeed;
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

    // damages
    public static float hardCandyDamage;
    public static float softCandyDamage;
    public static float gumDamage;
    public static float popRockDamage;
    public static float candyCornDamage;
    public static float prAreaDamage;
    public static float slowAmount;
    public static float stunAmount;

    public AudioClip[] soundClips; // Array to hold multiple sound clips
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        // progressBar = GameObject.Find("ProgressBar").GetComponent<Slider>();
        maxHealth = 10;
        
        currHealth = maxHealth;
        origSpeed = 3.0f;
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
        destinationSetter.target = player.transform;
    }

    // Update is called once per frame
    void Update()
    {
        path.maxSpeed = speed;

        if (currHealth <= 0)
        {
            Destroy(gameObject);
            // IncrementProgressBar();
            spawner.spawnInterval -= 0.1f;
            player.BroadcastMessage("IncrementScore", 5);
            
            // Clone the deathParticles and set its position to the enemy's position
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

        Destroy(clonedParticles, 5f);
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
<<<<<<< HEAD
            PlaySound(0);
            currHealth -= 5;
            
=======
            currHealth -= hardCandyDamage;
>>>>>>> 67af4f9759607ef69ae30b8c2082ca9c10ed533d
            Debug.Log("Hit with Hard Candy");
            Destroy(other.gameObject);
        } 
        else if (other.CompareTag("SoftCandy")) {
<<<<<<< HEAD
            PlaySound(0);
            currHealth -= 4;
            
=======
            currHealth -= softCandyDamage;
>>>>>>> 67af4f9759607ef69ae30b8c2082ca9c10ed533d
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
<<<<<<< HEAD
            PlaySound(0);
            currHealth -= 4;
=======
            currHealth -= candyCornDamage;
>>>>>>> 67af4f9759607ef69ae30b8c2082ca9c10ed533d
            Debug.Log("Hit with Hard Candy");
            Destroy(other.gameObject, 1.5f);
        }
        else if (other.CompareTag("PRCircle")) {
            currHealth -= prAreaDamage;
        }
    }
}
