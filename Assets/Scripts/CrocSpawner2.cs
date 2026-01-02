using UnityEngine;
using System.Collections;

public class CrocSpawner2 : MonoBehaviour
{
    public GameObject crocPrefab;
    public Transform spawnPoint;   // RIGHT side
    public Transform endPoint;     // LEFT side
    public float respawnDelay = 2f;

    void Start()
    {
        SpawnCroc();
    }

    void SpawnCroc()
    {
        GameObject croc = Instantiate(crocPrefab, spawnPoint.position, Quaternion.identity);

        CrocMove move = croc.GetComponent<CrocMove>();
        move.endPoint = endPoint;
        move.spawner = this;
    }

    public void Respawn()
    {
        StartCoroutine(RespawnDelay());
    }

    IEnumerator RespawnDelay()
    {
        yield return new WaitForSeconds(respawnDelay);
        SpawnCroc();
    }
}
