using UnityEngine;

public class EnemyProjectile2 : MonoBehaviour
{
    public float speed = 7f;
    public int damage = 25;
    public float lifeTime = 3f;
    public Transform endPoint;

    private Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        if (endPoint == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector2 target = endPoint.position;
        rb.linearVelocity = (target - (Vector2)transform.position).normalized * speed;

        if (Vector2.Distance(transform.position, endPoint.position) < 0.1f)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            DeerHealth health = other.GetComponent<DeerHealth>();
            if (health != null)
            {
                health.TakeDamage(damage);
            }

            Destroy(gameObject);
        }
    }
}
