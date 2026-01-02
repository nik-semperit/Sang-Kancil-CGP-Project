using UnityEngine;

public class CrocMovement : MonoBehaviour
{
    public Transform leftEndPoint;
    public float speed = 2f;

    private CrocSpawner spawner;

    public CrocSpawner Respawn { get; internal set; }

    public void Init(CrocSpawner spawnerRef)
    {
        spawner = spawnerRef;
    }

    void Update()
    {
        // Move LEFT
        transform.position = Vector2.MoveTowards(
            transform.position,
            leftEndPoint.position,
            speed * Time.deltaTime
        );

        // Face LEFT
        transform.localScale = new Vector3(-1, 1, 1);

        // Reached left end → disappear + respawn
        if (Vector2.Distance(transform.position, leftEndPoint.position) < 0.05f)
        {
            spawner.Respawn();
            Destroy(gameObject);
        }
    }
}
