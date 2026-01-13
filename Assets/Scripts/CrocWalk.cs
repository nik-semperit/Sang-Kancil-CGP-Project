using UnityEngine;

public class CrocWalk : MonoBehaviour
{
    public Transform leftPoint;
    public Transform rightPoint;
    public float patrolSpeed = 2f;

    private Transform target;
    private bool isPaused = false;

    void Start()
    {
        target = rightPoint;
    }

    void Update()
    {
        if (isPaused || leftPoint == null || rightPoint == null)
            return;

        transform.position = Vector2.MoveTowards(
            transform.position,
            new Vector2(target.position.x, transform.position.y),
            patrolSpeed * Time.deltaTime
        );

        if (Mathf.Abs(transform.position.x - target.position.x) < 0.05f)
        {
            SwitchTarget();
        }
    }

    void SwitchTarget()
    {
        if (target == rightPoint)
        {
            target = leftPoint;
            FaceLeft();
        }
        else
        {
            target = rightPoint;
            FaceRight();
        }
    }

    void FaceRight()
    {
        Vector3 scale = transform.localScale;
        scale.x = Mathf.Abs(scale.x);
        transform.localScale = scale;
    }

    void FaceLeft()
    {
        Vector3 scale = transform.localScale;
        scale.x = -Mathf.Abs(scale.x);
        transform.localScale = scale;
    }

    public void PauseMovement(bool pause)
    {
        isPaused = pause;
    }
}
