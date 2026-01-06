using UnityEngine;

public class EnemyProjectile2 : MonoBehaviour
{
    public float speed = 7f;
    public int damage = 25;
    public Transform endPoint;

    private Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (endPoint == null)
        {
            Destroy(gameObject);
            return;
        }

        // Move toward endpoint
        Vector2 dir = (endPoint.position - transform.position).normalized;
        rb.linearVelocity = dir * speed;

        // 🔥 STOP AT BOUNDARY
        if (Vector2.Distance(transform.position, endPoint.position) < 0.2f)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            DeerHealth player = other.GetComponent<DeerHealth>();
            if (player != null)
                player.TakeDamage(damage);

            Destroy(gameObject);
        }
    }
}
