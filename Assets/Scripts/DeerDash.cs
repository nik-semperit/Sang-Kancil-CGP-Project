using UnityEngine;

public class DeerDash : MonoBehaviour
{
    public float dashSpeed = 20f;
    public float dashDuration = 0.2f;
    public float doubleTapTime = 0.3f;

    private Rigidbody2D rb;
    private DeerMovement deer;
    private float lastLeftTapTime;
    private float lastRightTapTime;
    public bool isDashing;
    private float dashTimer;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        deer = GetComponent<DeerMovement>();
    }

    void Update()
    {
        if (isDashing)
        {
            dashTimer -= Time.deltaTime;
            if (dashTimer <= 0)
            {
                isDashing = false;
                rb.linearVelocity = new Vector2(0, rb.linearVelocity.y); // Stop dash
            }
            return;
        }

        // Detect double-tap LEFT
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            if (Time.time - lastLeftTapTime < doubleTapTime)
            {
                Dash(-1);
            }
            lastLeftTapTime = Time.time;
        }

        // Detect double-tap RIGHT
        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            if (Time.time - lastRightTapTime < doubleTapTime)
            {
                Dash(1);
            }
            lastRightTapTime = Time.time;
        }
    }

    void Dash(int direction)
    {
        isDashing = true;
        dashTimer = dashDuration;
        rb.linearVelocity = new Vector2(direction * dashSpeed, 0);
    }
}
