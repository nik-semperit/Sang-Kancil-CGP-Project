using UnityEngine;

public class FlowerShooter : MonoBehaviour
{
    public GameObject fireballPrefab;
    public Transform firePoint;
    public Transform fireballEndPoint;

    public float shootInterval = 2.5f;

    void Start()
    {
        InvokeRepeating(nameof(Shoot), 1f, shootInterval);
    }

    void OnDestroy()
    {
        CancelInvoke();
    }


    void Shoot()
    {
        if (fireballPrefab == null) return;
        if (firePoint == null) return;
        if (fireballEndPoint == null) return;

        GameObject fireball = Instantiate(
            fireballPrefab,
            firePoint.position,
            Quaternion.identity
        );

        EnemyProjectile2 proj =
            fireball.GetComponent<EnemyProjectile2>();

        if (proj != null)
        {
            proj.endPoint = fireballEndPoint;
        }
    }

}
