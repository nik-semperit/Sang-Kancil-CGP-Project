using UnityEngine;

public class CrocHideSeekAi : MonoBehaviour
{
    public Transform player;
    public float detectRange = 6f;
    public float stopDistance = 1.2f;
    public float chaseSpeed = 4f;
    public DeerMovement deer;

    private CrocWalk patrol;
    private float originalSpeed;

    void Start()
    {
        patrol = GetComponent<CrocWalk>();
        originalSpeed = patrol.patrolSpeed;
    }

    void Update()
    {
        if (player == null || patrol == null) return;

        // 🌿 Player hidden → NORMAL patrol, DO NOT stop
        if (deer != null && deer.isHiddenInBush)
        {
            patrol.patrolSpeed = originalSpeed;
            return;
        }

        float distance = Vector2.Distance(transform.position, player.position);

        // 🐊 Chase when detected
        if (distance <= detectRange)
        {
            patrol.patrolSpeed = chaseSpeed;
        }
        else
        {
            patrol.patrolSpeed = originalSpeed;
        }
    }



}
