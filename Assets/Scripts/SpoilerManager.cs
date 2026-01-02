using UnityEngine;
using System.Collections;

public class CrocSpawner : MonoBehaviour
{
    public GameObject crocPrefab;
    public Transform spawnPoint;
    public Transform leftEndPoint;
    public float respawnDelay = 2f;

    private bool crocAlive = false;

    void Start()
    {
        SpawnCroc();
    }

    public void SpawnCroc()
    {
        if (crocAlive) return;

        GameObject croc = Instantiate(crocPrefab, spawnPoint.position, Quaternion.identity);

        CrocMovement move = croc.GetComponent<CrocMovement>();
        move.leftEndPoint = leftEndPoint;
        move.spawner = this;

        crocAlive = true;
    }

    public IEnumerator Respawn()
    {
        crocAlive = false;
        yield return new WaitForSeconds(respawnDelay);
        SpawnCroc();
    }
}
