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

    [Header("Jump Settings")]
    public int extraJumpsValue = 1;   // Default = Double Jump
    private int extraJumps;

    // One-time triple jump
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
        bool isGrounded = Physics2D.OverlapCircle(
            groundCheck.position, checkRadius, groundLayer);

        // Reset jumps when grounded
        if (isGrounded)
            extraJumps = extraJumpsValue;

        // ================= DASH INPUT =================
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

        // ================= MOVEMENT =================
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

        // ================= JUMP =================
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

                // Reset triple jump after one use
                if (extraJumps <= 0 && oneTimeTripleJump)
                {
                    extraJumpsValue = 1;
                    oneTimeTripleJump = false;
                }
            }
        }

        // ================= FACING DIRECTION =================
        if (moveInput > 0)
            transform.rotation = Quaternion.Euler(0, 180, 0);
        else if (moveInput < 0)
            transform.rotation = Quaternion.Euler(0, 0, 0);

        // ================= ANIMATION =================
        anim.SetFloat("Speed", Mathf.Abs(moveInput));
        anim.SetBool("IsGrounded", isGrounded);

        // ================= CROUCH =================
        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        {
            if (standingCollider != null)
            {
                standingCollider.size =
                    new Vector2(standingCollider.size.x, crouchHeight);
                standingCollider.offset =
                    new Vector2(0, (crouchHeight - originalHeight) / 2);
            }
            anim.SetBool("IsCrouching", true);
        }
        else
        {
            if (standingCollider != null)
            {
                standingCollider.size =
                    new Vector2(standingCollider.size.x, originalHeight);
                standingCollider.offset = Vector2.zero;
            }
            anim.SetBool("IsCrouching", false);
        }

        // ================= DASH TIMER =================
        if (isDashing)
        {
            dashTimer -= Time.deltaTime;
            if (dashTimer <= 0f)
                isDashing = false;
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

    void StartDash(int direction)
    {
        isDashing = true;
        dashDirection = direction;
        dashTimer = dashDuration;
        lastDashTime = Time.time;

        rb.linearVelocity = Vector2.zero;
    }

    public void Bounce()
    {
        if (hurtSound && audioSource)
            audioSource.PlayOneShot(hurtSound);

        rb.linearVelocity = Vector2.zero;
        anim.SetTrigger("JumpTrigger");

        float randomX = Random.Range(-bounceSideForce, bounceSideForce);
        rb.AddForce(new Vector2(randomX, bounceForce), ForceMode2D.Impulse);
    }

    // ================= DAMAGE CROC =================
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
                    CrocHealth2 croc =
                        collision.gameObject.GetComponent<CrocHealth2>();

                    if (croc != null)
                        croc.TakeDamage(10);

                    rb.linearVelocity =
                        new Vector2(rb.linearVelocity.x, jumpForce * 0.6f);

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
