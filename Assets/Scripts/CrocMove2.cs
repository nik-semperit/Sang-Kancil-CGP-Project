using UnityEngine;

public class CrocMove2 : MonoBehaviour
{
    public float speed = 2f;
    public Transform endPoint;

    float waterY;

    void Start()
    {
        waterY = transform.position.y;
    }

    void Update()
    {
        transform.position = Vector2.MoveTowards(
            transform.position,
            new Vector2(endPoint.position.x, waterY),
            speed * Time.deltaTime
        );

        if (Vector2.Distance(transform.position, endPoint.position) < 0.1f)
        {
            Destroy(gameObject);
        }
    }
}
