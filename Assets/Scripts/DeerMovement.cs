using UnityEngine;

public class DeerMovement : MonoBehaviour
{
    [Header("Crouch")]
    public BoxCollider2D standingCollider; 
    public float crouchHeight = 0.5f;      
    public float originalHeight;           

    [Header("Audio")]
    public AudioSource audioSource; 
    public AudioClip jumpSound;     
    public AudioClip hurtSound;     

    [Header("Movement")]
    public float moveSpeed = 5f;
    public float jumpForce = 10f; 
    public float bounceForce = 1f;
    public float bounceSideForce = 1f;

    [Header("Dash")]
    public float dashSpeed = 15f;
    public float dashDuration = 0.15f;
    public float dashCooldown = 0.5f;
    public float doubleTapTime = 0.25f;

    private bool isDashing;
    private float dashTimer;
    private float lastDashTime;

    private float lastLeftTapTime;
    private float lastRightTapTime;
    private int dashDirection;


    [Header("Double Jump")]
    public int extraJumpsValue = 1; 
    private int extraJumps;

    [Header("Physics & Detection")]
    public Rigidbody2D rb;
    public Transform groundCheck;
    public LayerMask groundLayer;
    public float checkRadius = 0.2f;

    [Header("Ground Effects")]
    public GameObject groundMoveEffect;
    public Transform effectSpawnPoint;

    [Header("Jump Effect")]
    public GameObject jumpEffect;
    public Transform jumpEffectPoint; // Empty GameObject under feet



    // Animation
    public Animator anim;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        extraJumps = extraJumpsValue; 

        if (standingCollider != null)
            originalHeight = standingCollider.size.y;
    }

    void Update()
    {
        // 1. INPUT & PHYSICS CHECKS
        float moveInput = Input.GetAxisRaw("Horizontal");
        bool isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, groundLayer);

        if (isGrounded)
        {
            extraJumps = extraJumpsValue;
        }

        // 2. MOVEMENT LOGIC
        if (isDashing)
        {
            rb.linearVelocity = new Vector2(dashDirection * dashSpeed, 0f);
        }
        else if (!anim.GetBool("IsCrouching"))
        {
            rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);
        }
        else
        {
            rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
        }

        // 3. JUMP LOGIC
        if (Input.GetButtonDown("Jump"))
        {
            if (isGrounded)
            {
                PerformJump();
            }
            else if (extraJumps > 0)
            {
                PerformJump();
                extraJumps--; 
            }
        }

        // DASH INPUT (Double Tap Left / Right Arrow)
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (Time.time - lastLeftTapTime <= doubleTapTime &&
                Time.time >= lastDashTime + dashCooldown)
            {
                StartDash(-1);
            }
            lastLeftTapTime = Time.time;
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (Time.time - lastRightTapTime <= doubleTapTime &&
                Time.time >= lastDashTime + dashCooldown)
            {
                StartDash(1);
            }
            lastRightTapTime = Time.time;
        }


        // 4. FLIPPING & SQUISHING (FIXED LOGIC)

        // A. Handle Squish (Crouching) - ALWAYS KEEP SCALE POSITIVE
        // We stop using negative scale for direction here. Scale is only for size.
        float currentSizeX = anim.GetBool("IsCrouching") ? 0.7125f : 1f; 
        transform.localScale = new Vector3(currentSizeX, 1, 1); 

        // B. Handle Direction (Rotation)
        // Rotating the Y-axis moves the child objects (mouth box) along with the sprite.
        if (moveInput > 0) // Moving Right
        {
            // Rotate 180 degrees to face Right (since your art faces Left by default)
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else if (moveInput < 0) // Moving Left
        {
            // Rotate back to 0 (Default Left)
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        // If moveInput is 0, we do nothing, so it stays facing the last direction.

        
        // 5. UPDATE ANIMATOR   
        anim.SetFloat("Speed", Mathf.Abs(moveInput)); 
        anim.SetBool("IsGrounded", isGrounded);

        // Crouch Logic
        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        {
            if (standingCollider != null)
            {
                Vector2 size = standingCollider.size;
                size.y = crouchHeight; 
                standingCollider.size = size;
                standingCollider.offset = new Vector2(0, (crouchHeight - originalHeight) / 2);
            }
            anim.SetBool("IsCrouching", true);
        }
        else 
        {
            if (standingCollider != null)
            {
                Vector2 size = standingCollider.size;
                size.y = originalHeight;
                standingCollider.size = size;
                standingCollider.offset = Vector2.zero;
            }
            anim.SetBool("IsCrouching", false);
        }

        // 6. GROUND MOVE EFFECT
        if (isGrounded && Mathf.Abs(moveInput) > 0.1f)
        {
            Instantiate(groundMoveEffect, effectSpawnPoint.position, Quaternion.identity);
        }

        // DASH TIMER
        if (isDashing)
        {
            dashTimer -= Time.deltaTime;
            if (dashTimer <= 0f)
            {
                isDashing = false;
            }
        }


    }

    void PerformJump()
    {
        if (jumpEffect != null && jumpEffectPoint != null)
        {
            Instantiate(jumpEffect, jumpEffectPoint.position, Quaternion.identity);
        }

        if (jumpSound != null && audioSource != null)
            audioSource.PlayOneShot(jumpSound);

        anim.ResetTrigger("JumpTrigger"); 
        anim.SetTrigger("JumpTrigger");
    
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f); 
        rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
    }

    void StartDash(int direction)
    {
        isDashing = true;
        dashDirection = direction;
        dashTimer = dashDuration;
        lastDashTime = Time.time;

        // Cancel vertical movement for a sharp dash
        rb.linearVelocity = Vector2.zero;

        // Optional animation trigger
        // anim.SetTrigger("Dash");
    }


    public void Bounce()
    {
        if (hurtSound != null && audioSource != null)
             audioSource.PlayOneShot(hurtSound);
        
        rb.linearVelocity = Vector2.zero; 
        anim.SetTrigger("JumpTrigger");

        float randomX = Random.Range(-bounceSideForce, bounceSideForce);
        rb.AddForce(new Vector2(randomX, bounceForce), ForceMode2D.Impulse);
    }



    private float damageCooldown = 0.5f;
    private float damageTimer = 0f;

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Enemy"))
            return;

        // Only when deer is falling or standing
        if (rb.linearVelocity.y > 1f)
            return;

        foreach (ContactPoint2D contact in collision.contacts)
        {
            // Check if standing on top of croc
            if (contact.normal.y > 0.5f)
            {
                damageTimer += Time.deltaTime;

                if (damageTimer >= damageCooldown)
                {
                    CrocHealth2 crocHealth =
                        collision.gameObject.GetComponent<CrocHealth2>();

                    if (crocHealth != null)
                    {
                        crocHealth.TakeDamage(10);
                    }

                    // small bounce
                    rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce * 0.6f);

                    damageTimer = 0f;
                }

                break;
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            damageTimer = 0f;
        }
    }



}