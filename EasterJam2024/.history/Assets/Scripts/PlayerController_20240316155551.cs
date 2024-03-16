using System.Collections.Generic;
using System.Data.Common;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerContoller : MonoBehaviour
{
    public List<string> candiesPlayerHas;
    public List<string> candiesPlayerDoesntHave;
    public Vector2 playerInput;
    Rigidbody2D rb;
    public float moveSpeed;
    float inputHorizontal;
    bool facingRight = true;
    public bool isFlipped = true;

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
        // Debug.Log(Input.GetAxisRaw("Horizontal"));
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

    void flip() {
        Vector3 currentScale = gameObject.transform.localScale;
        currentScale.x *= -1;
        gameObject.transform.localScale = currentScale;

        facingRight = !facingRight;
    }
}
