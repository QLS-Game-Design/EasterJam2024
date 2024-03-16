using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public List<GameObject> arrowPrefabs; 
    public Transform playerTransform;
    public float distanceFromPlayer = 0.1f;
    public float arrowSpeed = 20f;
    public float fireCooldown = 0.005f; 

    private Vector2 direction;
    private float fireTimer; 

    void Update()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        direction = (mousePosition - (Vector2)playerTransform.position).normalized;

        Vector2 newPos = (Vector2)playerTransform.position + direction * distanceFromPlayer;
        transform.position = newPos;

       
        fireTimer -= Time.deltaTime;

        if (Input.GetMouseButton(0) && fireTimer <= 0)
        {
            FireArrow();
            fireTimer = fireCooldown; 
        }
    }

    void FireArrow()
    {
        GameObject arrowPrefab = arrowPrefabs[Random.Range(0, arrowPrefabs.Count)];

        GameObject arrow = Instantiate(arrowPrefab, transform.position, Quaternion.identity);
        Rigidbody2D rb = arrow.GetComponent<Rigidbody2D>();
        rb.velocity = direction * arrowSpeed;
        Destroy(arrow, 7f);
    }
}