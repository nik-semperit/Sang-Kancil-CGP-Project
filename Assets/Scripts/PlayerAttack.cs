using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [Header("Shooting Settings")]
    public Transform firePoint;      
    public GameObject bulletPrefab;  
    public float fireRate = 0.5f;    
    private float nextFireTime = 0f;

    void Update()
    {
        // Changed from "Fire1" to KeyCode.F
        if (Input.GetKeyDown(KeyCode.F) && Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + fireRate;
        }
    }

    void Shoot()
    {
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }

    

}