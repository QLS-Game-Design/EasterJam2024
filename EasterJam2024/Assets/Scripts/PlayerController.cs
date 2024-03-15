using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public List<string> candiesPlayerHas;
    public List<string> candiesPlayerDoesntHave;
    public Vector2 playerInput;
    Rigidbody2D rb;
    public float moveSpeed;
    float inputHorizontal;
    bool facingRight = true;

    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        //anim = GetComponent<Animator>();
        candiesPlayerHas.Add("HardCandy");
        candiesPlayerDoesntHave.Add("SoftCandy");
        candiesPlayerDoesntHave.Add("Gum");
    }

    // Update is called once per frame
    void Update()
    {
        playerInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        rb.velocity = playerInput.normalized * moveSpeed;

        inputHorizontal = Input.GetAxisRaw("Horizontal");
        if (inputHorizontal > 0 && !facingRight) {
            flip();
        }
        if (inputHorizontal < 0 && facingRight) {
            flip();
        }

        if (Input.GetAxisRaw("Horizontal") == 0 && Input.GetAxisRaw("Vertical") == 0) {
            anim.SetBool("isRunning", false);
        } else {
            anim.SetBool("isRunning", true);
        }
    }

    void flip() {
        Vector3 currentScale = gameObject.transform.localScale;
        currentScale.x *= -1;
        gameObject.transform.localScale = currentScale;

        facingRight = !facingRight;
    }
}
