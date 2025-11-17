using UnityEngine;
using System.Collections; // Required for IEnumerator

public class SpawnManager : MonoBehaviour
{
    public GameObject healthPickupPrefab; // Drag your Health Pickup Prefab here in the Inspector
    public float minSpawnTime = 5f;
    public float maxSpawnTime = 15f;
    public float xRange = 8f; // Max X coordinate for spawning
    public float yRange = 4f; // Max Y coordinate for spawning

    void Start()
    {
        StartCoroutine(SpawnHealthRoutine());
    }

    IEnumerator SpawnHealthRoutine()
    {
        // Add an initial wait time if needed
        yield return new WaitForSeconds(1.5f);

        while (true) // Infinite loop to keep spawning
        {
            // Generate a random position within screen/game boundaries
            Vector3 posToSpawn = new Vector3(Random.Range(-xRange, xRange), Random.Range(-yRange, yRange), 0);

            // Instantiate the pickup at the random position
            Instantiate(healthPickupPrefab, posToSpawn, Quaternion.identity);

            // Wait for a random interval before the next spawn
            float randomWaitTime = Random.Range(minSpawnTime, maxSpawnTime);
            yield return new WaitForSeconds(randomWaitTime);
        }
    }
}

