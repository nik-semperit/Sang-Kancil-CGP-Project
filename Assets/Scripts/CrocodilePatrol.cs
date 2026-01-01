using UnityEngine;
using UnityEngine.UI; // <--- 1. ADD THIS LINE AT THE VERY TOP!



public class CrocodilePatrol : MonoBehaviour
{

    [Header("Effects")]
    public GameObject deathParticlePrefab; // Drag the particle prefab here

    [Header("Audio")]
    public AudioSource audioSource; // You need to declare this to use it!
    public AudioClip deathSFX;    // The specific sound file

    public float speed = 2f;
    public float turnDistance = 2f; // Start with 2, we might need to increase this!
    public Transform pointLeft;
    public Transform pointRight;
    public float scaleMultiplier; // Set this to the size you want (e.g., 2, 3, 4)
        
    [Header("Health & UI")]
    public int maxHealth = 10;
    public int currentHealth = 10;
    public int damageToDeal = 1;
    public Slider healthBar;
    public Image fillImage; // Add using UnityEngine.UI; at the top if needed
    
    private Rigidbody2D rb;
    private Animator anim;
    private Transform currentTarget;

    [Header("Shooting")]
    public GameObject fireballPrefab;
    public Transform firePoint; // Where the bullet comes out (Mouth)
    public float fireRate; // Shoot every 2 seconds
    private float nextFireTime;


    void Start()
    {
        currentHealth = 10;
        Debug.Log("Start() Crocodile Health: " + currentHealth);
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        
        // Start by walking towards the right point
        currentTarget = pointRight;
        if (healthBar != null)
        {
            healthBar.maxValue = 10;
            healthBar.value = 10;
        }
    }

    void Update()
    {
        // 1. Move the Crocodile
        // Determine direction based on the current target
        float direction = (currentTarget == pointRight) ? speed : -speed;
        rb.linearVelocity = new Vector2(direction, rb.linearVelocity.y);

        // 2. Flipping (Visual)
        if (direction > 0) {
            transform.localScale = new Vector3(-1, 1, 1); // Face Right
            transform.localScale = new Vector3(scaleMultiplier * -1, scaleMultiplier, 1);
        }
        else
        {
            transform.localScale = new Vector3(1, 1, 1);  // Face Left
            transform.localScale = new Vector3(scaleMultiplier, scaleMultiplier, 1);
        }
        // 3. Check Distance to Target
        if (Vector2.Distance(transform.position, currentTarget.position) < 1f)
        {
            // We arrived! Switch the target.
            if (currentTarget == pointRight)
                currentTarget = pointLeft;
            else
                currentTarget = pointRight;
        }

        // 4. Animation Logic
        // The crocodile is always moving while patrolling, so the Walk animation should play.
        anim.SetBool("IsMoving", true);


        // SHOOTING LOGIC
        // If current time is past the "next fire time", SHOOT!
        if (Time.time > nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + fireRate; // Reset timer
        }

    }


        public void TakeDamage(int damage)
            {
                currentHealth -= damage;

                Debug.Log("Crocodile Health: " + currentHealth);
                
                Debug.Log("Hit");
                anim.SetTrigger("Hit");

                if (currentHealth <= 0)
                {
                    Die();
                    healthBar.fillRect.gameObject.SetActive(false);
                }
                    healthBar.value = currentHealth;
        

            }

       void Die()
{
    // SAFETY CHECK: Only play sound if the file actually exists
    if (deathSFX != null && audioSource != null)
    {
        audioSource.PlayOneShot(deathSFX);
    }

    // SAFETY CHECK: Only spawn particles if the prefab actually exists
    if (deathParticlePrefab != null)
    {
        Instantiate(deathParticlePrefab, transform.position, Quaternion.identity);
    }

    // KILL THE BOSS (This will now run even if the stuff above fails!)
    Destroy(gameObject);
}


        void Shoot()
        {
        if (fireballPrefab != null && firePoint != null)
        {
            // Determine direction based on where the croc is facing
            // If facing left (-1 scale), we need to rotate the bullet 180 degrees
            Quaternion bulletRotation = transform.localScale.x < 0 ? Quaternion.Euler(0, 180, 0) : Quaternion.identity;

            Instantiate(fireballPrefab, firePoint.position, bulletRotation);
        }
        }


        

        
}