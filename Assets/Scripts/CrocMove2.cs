using UnityEngine;

public class CrocMove : MonoBehaviour
{
    public float speed = 2f;
    public Transform endPoint;     // LEFT side
    [HideInInspector] public CrocSpawner2 spawner;

    void Update()
    {
        transform.position = Vector2.MoveTowards(
            transform.position,
            endPoint.position,
            speed * Time.deltaTime
        );

        // When reach left end → despawn
        if (Vector2.Distance(transform.position, endPoint.position) < 0.05f)
        {
            spawner.Respawn();
            Destroy(gameObject);
        }
    }
}
