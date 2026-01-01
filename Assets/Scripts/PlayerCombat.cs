using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [Header("Shooting Settings")]
    public Transform firePoint;       // Where the bullet comes out (Mouth/Horn)
    public GameObject bulletPrefab;   // The "Purify Shot" Prefab
    public float fireRate = 0.5f;     // How fast you can spam shoot

    private float nextFireTime = 0f;

    void Update()
    {
        // Change KeyCode.F to whatever button you want (e.g., KeyCode.J or "Fire1")
        if (Input.GetKeyDown(KeyCode.F) && Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + 1f / fireRate; // Reset cooldown
        }
    }

    void Shoot()
    {
        // 1. Create the bullet at the FirePoint position
        // 2. Use firePoint.rotation so it faces the same way as the player
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        
        // Optional: Play a sound here later
        // AudioSource.PlayClipAtPoint(shootSound, transform.position);
    }
}