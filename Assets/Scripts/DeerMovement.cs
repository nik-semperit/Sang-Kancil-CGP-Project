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

    [Header("Double Jump")]
    public int extraJumpsValue = 1; 
    private int extraJumps;

    [Header("Physics & Detection")]
    public Rigidbody2D rb;
    public Transform groundCheck;
    public LayerMask groundLayer;
    public float checkRadius = 0.2f; 

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

    // --- UPDATE LOOP STARTS ---
    void Update()
    {
        // 1. INPUT & PHYSICS CHECKS
        float moveInput = Input.GetAxisRaw("Horizontal");
        bool isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, groundLayer);

        // --- DOUBLE JUMP RESET LOGIC ---
        if (isGrounded)
        {
            extraJumps = extraJumpsValue;
        }

        // 2. MOVEMENT LOGIC
        // Only move if NOT crouching (Optional: prevents sliding while ducking)
        if (!anim.GetBool("IsCrouching")) 
        {
            rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);
        }
        else 
        {
            rb.linearVelocity = new Vector2(0, rb.linearVelocity.y); // Stop moving if crouching
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
        
// 4. FLIPPING & SQUISHING (Fixed Logic)
        
        // A. Determine the base size (Squished if crouching, Normal if standing)
        // We use the boolean directly from the Animator to know if we are crouching
        float sizeX = anim.GetBool("IsCrouching") ? 0.7125f : 1f; 

        // B. Apply Direction + Size
        // Note: Your original code used -1 for Right, so I kept that logic.
        if (moveInput > 0) // Moving Right
        {
            transform.localScale = new Vector3(-sizeX, 1, 1);
        }
        else if (moveInput < 0) // Moving Left
        {
            transform.localScale = new Vector3(sizeX, 1, 1);
        }
        
        else 
        {
            // If standing still, keep facing the current direction but apply the squish
            // Math.Sign gets the current direction (-1 or 1)
            float currentDir = Mathf.Sign(transform.localScale.x);
            transform.localScale = new Vector3(currentDir * Mathf.Abs(sizeX), 1, 1);
        }

        
        // 5. UPDATE ANIMATOR   
        anim.SetFloat("Speed", Mathf.Abs(moveInput)); 
        anim.SetBool("IsGrounded", isGrounded);

        // Crouch Logic here
        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        {
            // Shrink
            if (standingCollider != null)
            {
                Vector2 size = standingCollider.size;
                size.y = crouchHeight; 
                standingCollider.size = size;
                
                standingCollider.offset = new Vector2(0, (crouchHeight - originalHeight) / 2);
            }

            // Play Animation
            anim.SetBool("IsCrouching", true);
        }
        else 
        {
            // Stand up for second phase of animation
            if (standingCollider != null)
            {
                Vector2 size = standingCollider.size;
                size.y = originalHeight;
                standingCollider.size = size;
                standingCollider.offset = Vector2.zero; // Reset
            }

            anim.SetBool("IsCrouching", false);
        }

    } // <--- THIS BRACKET WAS MISSING/WRONG! UPDATE ENDS HERE.


    // --- FUNCTIONS OUTSIDE UPDATE ---

    void PerformJump()
    {
        if (jumpSound != null && audioSource != null)
            audioSource.PlayOneShot(jumpSound);

        anim.ResetTrigger("JumpTrigger"); 
        anim.SetTrigger("JumpTrigger");
    
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f); 
        rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
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

} // CLASS ENDS