using UnityEngine;
using System.Collections;

public class CrocSpawner2 : MonoBehaviour
{
    public GameObject crocPrefab;
    public Transform spawnPoint;
    public Transform endPoint;

    public float spawnInterval = 7.5f;

    public float minSpeed = 5.5f;
    public float maxSpeed = 6.5f;

    void Start()
    {
        StartCoroutine(SpawnLoop());
    }

    IEnumerator SpawnLoop()
    {
        while (true)
        {
            SpawnCroc();
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    void SpawnCroc()
    {
        GameObject croc = Instantiate(crocPrefab, spawnPoint.position, Quaternion.identity);

        CrocMove2 move = croc.GetComponent<CrocMove2>();
        move.endPoint = endPoint;

        // 🎯 RANDOM SPEED PER CROC
        move.speed = Random.Range(minSpeed, maxSpeed);
    }
}
