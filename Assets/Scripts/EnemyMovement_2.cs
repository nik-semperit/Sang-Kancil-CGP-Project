using UnityEngine;

public class EnemyMovement_2 : MonoBehaviour
{
    public Transform leftPoint;
    public Transform rightPoint;
    public float speed = 2f;

    private Rigidbody2D rb;
    private bool movingRight = true;

    // Expose current movement speed (+speed or -speed)
    public float CurrentSpeed => movingRight ? speed : -speed;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;
        rb.freezeRotation = true;
    }

    void FixedUpdate()
    {
        // Move using physics (better for platforms)
        rb.MovePosition(rb.position + new Vector2(CurrentSpeed, 0) * Time.fixedDeltaTime);

        // Turning points
        if (movingRight && transform.position.x >= rightPoint.position.x)
            movingRight = false;

        else if (!movingRight && transform.position.x <= leftPoint.position.x)
            movingRight = true;

        // Flip sprite
        Vector3 scale = transform.localScale;
        scale.x = movingRight ? -1 : 1;
        transform.localScale = scale;
    }
}