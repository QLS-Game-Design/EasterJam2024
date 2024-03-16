using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public GameObject arrowPrefab;
    public Transform playerTransform;
    public float distanceFromPlayer = 0.1f;
    public float arrowSpeed = 20f;

    private Vector2 direction;

    void Update()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        bowTransform.rotation = Quaternion.Euler(0, 0, angle);

        if (Input.GetMouseButtonDown(0))
        {
            FireArrow();
        }
    }

    void FireArrow()
    {
        GameObject arrow = Instantiate(arrowPrefab, transform.position, Quaternion.identity);
        Rigidbody2D rb = arrow.GetComponent<Rigidbody2D>();
        rb.velocity = direction * arrowSpeed;
        Destroy(arrow, 7f);
    }
}
