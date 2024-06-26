using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public List<GameObject> arrowPrefabs; 
    public Transform playerTransform;
    public float distanceFromPlayer = 0.1f;
    public float arrowSpeed = 20f;
    public float fireCooldown = 0.5f; 

    private Vector2 direction;
    private float fireTimer; 
    private GameObject player;
    private PlayerController playerController;
    public class Ant_Eat : MonoBehaviour

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerController = player.GetComponent<PlayerController>();
    }
void Update() {
    Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    direction = (mousePosition - (Vector2)playerTransform.position).normalized;

    Vector2 newPos = (Vector2)playerTransform.position + direction * distanceFromPlayer;
    transform.position = newPos;


    float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
    transform.rotation = Quaternion.Euler(0, 0, angle);

    fireTimer -= Time.deltaTime;

    if (Input.GetMouseButtonDown(0) && fireTimer <= 0)
    {
        FireArrow();
        fireTimer = fireCooldown; 
    }

    if (playerController.isFlipped == false) {
        Vector3 currentScale = gameObject.transform.localScale;
        currentScale.x *= -1;
        gameObject.transform.localScale = currentScale;
    }
    
}
    void FireArrow()
    {
        GameObject arrowPrefab = arrowPrefabs[Random.Range(0, arrowPrefabs.Count)];

        GameObject arrow = Instantiate(arrowPrefab, transform.position, Quaternion.identity);
        Rigidbody2D rb = arrow.GetComponent<Rigidbody2D>();
        rb.velocity = direction * arrowSpeed;
        
        Destroy(arrow, 3f);
    }
}