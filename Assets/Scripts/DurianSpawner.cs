using UnityEngine;
using System.Collections; 

public class DurianSpawner : MonoBehaviour
{
    public GameObject durianPrefab; // Drag the Durian Prefab here

    // Randomize the timing so trees don't drop in sync
    public float minSpawnTime = 2f; 
    public float maxSpawnTime = 12f;

    void Start()
    {
        // Start the infinite loop
        StartCoroutine(DropDurianRoutine());
    }

    IEnumerator DropDurianRoutine()
    {
        while (true) // Run forever
        {
            // 1. Wait for a random time (Creates the "Natural" feel)
            float waitTime = Random.Range(minSpawnTime, maxSpawnTime);
            yield return new WaitForSeconds(waitTime);

            // 2. Spawn the Durian at this object's position
            Instantiate(durianPrefab, transform.position, Quaternion.identity);
        }
    }
}