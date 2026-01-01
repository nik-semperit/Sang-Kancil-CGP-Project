using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement_2 : MonoBehaviour
{
    float moveInput;
    public Rigidbody2D rb;
    public float speed;
    public Transform pos;         // ground check position
    public float radius;          // ground check radius
    public LayerMask groundLayers;
    public float jumpforce;
    public float heightCutter;
    bool grounded;

    // --- ANIMATION ---
    private Animator anim;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();   // <-- Get animator here
    }

    void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(moveInput * speed, rb.linearVelocity.y);

        // Flip character depending on direction
        if (moveInput > 0f)
            transform.rotation = Quaternion.Euler(0, 180, 0);
        else if (moveInput < 0f)
            transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    void Update()
    {
        moveInput = Input.GetAxis("Horizontal");

        // --- Ground check ---
        grounded = Physics2D.OverlapCircle(pos.position, radius, groundLayers);

        // --- Jump input ---
        if (grounded && Input.GetKeyDown(KeyCode.Space))
        {
            rb.linearVelocity = Vector2.up * jumpforce;

            // 🔥 Trigger jump animation
            anim.SetTrigger("JumpTrigger");
        }

        // --- Variable jump height ---
        if (Input.GetKeyUp(KeyCode.Space))
        {
            if (rb.linearVelocity.y > 0)
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, rb.linearVelocity.y * heightCutter);
        }

        // 🔥 Update Animation Parameters
        anim.SetFloat("Speed", Mathf.Abs(moveInput));  // idle ↔ run
        anim.SetBool("isJumping", !grounded);          // grounded ↔ jump
    }
}
