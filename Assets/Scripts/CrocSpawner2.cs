using UnityEngine;
using System.Collections;

public class CrocSpawner2 : MonoBehaviour
{
    [Header("References")]
    public GameObject crocPrefab;
    public Transform spawnPoint;
    public Transform endPoint;

    [Header("Spawn Settings")]
    public float startSpawnInterval = 5f;
    public float minSpawnInterval = 1.5f;

    [Header("Speed Settings")]
    public float startSpeed = 2.5f;
    public float maxSpeed = 7f;

    [Header("Difficulty Ramp")]
    public float rampDuration = 25f;

    [Header("Wave Settings")]
    public int minCrocs = 2;
    public int maxCrocs = 5;
    public float spacingMin = 1.5f;
    public float spacingMax = 3f;

    private bool rampStarted = false;
    private float rampTimer = 0f;

    void Start()
    {
        StartCoroutine(SpawnLoop());
    }

    void Update()
    {
        if (rampStarted)
        {
            rampTimer += Time.deltaTime;
        }
    }

    IEnumerator SpawnLoop()
    {
        while (true)
        {
            float t = GetRampT();
            float spawnInterval = Mathf.Lerp(
                startSpawnInterval,
                minSpawnInterval,
                t
            );

            SpawnWave(t);
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    void SpawnWave(float t)
    {
        int count = Random.Range(minCrocs, maxCrocs + 1);
        float offset = 0f;

        for (int i = 0; i < count; i++)
        {
            Vector3 pos = spawnPoint.position;
            pos.x -= offset;

            GameObject croc = Instantiate(crocPrefab, pos, Quaternion.identity);

            CrocMove2 move = croc.GetComponent<CrocMove2>();
            if (move != null)
            {
                move.endPoint = endPoint;
                move.speed = Mathf.Lerp(startSpeed, maxSpeed, t);
            }

            offset += Random.Range(spacingMin, spacingMax);
        }
    }

    float GetRampT()
    {
        if (!rampStarted) return 0f;
        return Mathf.Clamp01(rampTimer / rampDuration);
    }

    // 🔥 CALLED ONCE BY RIVER TRIGGER
    public void StartDifficultyRamp()
    {
        if (rampStarted) return;
        rampStarted = true;
        rampTimer = 0f;
    }
}
