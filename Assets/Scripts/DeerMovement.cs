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

    [Header("Jump Settings")]
    public int extraJumpsValue = 1;   // 🔹 DEFAULT = DOUBLE JUMP
    private int extraJumps;

    // 🔥 ONE-TIME TRIPLE JUMP CONTROL
    [HideInInspector] public bool oneTimeTripleJump = false;

    [Header("Physics & Detection")]
    public Rigidbody2D rb;
    public Transform groundCheck;
    public LayerMask groundLayer;
    public float checkRadius = 0.2f;

    public Animator anim;

    // Damage to croc
    private float damageCooldown = 0.5f;
    private float damageTimer = 0f;

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
        float moveInput = Input.GetAxisRaw("Horizontal");
        bool isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, groundLayer);

        // 🔁 Reset jumps when grounded
        if (isGrounded)
        {
            extraJumps = extraJumpsValue;
        }

        // Movement
        if (!anim.GetBool("IsCrouching"))
            rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);
        else
            rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);

        // Jump
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

                // 🔥 AFTER USING TRIPLE JUMP ONCE → RESET
                if (extraJumps <= 0 && oneTimeTripleJump)
                {
                    extraJumpsValue = 1;   // BACK TO DOUBLE JUMP
                    oneTimeTripleJump = false;
                }
            }
        }

        // Direction
        if (moveInput > 0)
            transform.rotation = Quaternion.Euler(0, 180, 0);
        else if (moveInput < 0)
            transform.rotation = Quaternion.Euler(0, 0, 0);

        anim.SetFloat("Speed", Mathf.Abs(moveInput));
        anim.SetBool("IsGrounded", isGrounded);

        // Crouch
        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        {
            if (standingCollider != null)
            {
                standingCollider.size = new Vector2(standingCollider.size.x, crouchHeight);
                standingCollider.offset = new Vector2(0, (crouchHeight - originalHeight) / 2);
            }
            anim.SetBool("IsCrouching", true);
        }
        else
        {
            if (standingCollider != null)
            {
                standingCollider.size = new Vector2(standingCollider.size.x, originalHeight);
                standingCollider.offset = Vector2.zero;
            }
            anim.SetBool("IsCrouching", false);
        }
    }

    void PerformJump()
    {
        if (jumpSound && audioSource)
            audioSource.PlayOneShot(jumpSound);

        anim.SetTrigger("JumpTrigger");
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f);
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }

    public void Bounce()
    {
        // Play hurt sound
        if (hurtSound != null && audioSource != null)
            audioSource.PlayOneShot(hurtSound);

        // Reset current velocity
        rb.linearVelocity = Vector2.zero;

        // Trigger jump animation
        anim.SetTrigger("JumpTrigger");

        // Add upward + slight sideways force
        float randomX = Random.Range(-bounceSideForce, bounceSideForce);
        rb.AddForce(new Vector2(randomX, bounceForce), ForceMode2D.Impulse);
    }


    // 🔥 DAMAGE CROC WHEN STANDING ON IT
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Enemy"))
            return;

        if (rb.linearVelocity.y > 1f)
            return;

        foreach (ContactPoint2D contact in collision.contacts)
        {
            if (contact.normal.y > 0.5f)
            {
                damageTimer += Time.deltaTime;

                if (damageTimer >= damageCooldown)
                {
                    CrocHealth2 croc = collision.gameObject.GetComponent<CrocHealth2>();
                    if (croc != null)
                        croc.TakeDamage(10);

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
            damageTimer = 0f;
    }
}
