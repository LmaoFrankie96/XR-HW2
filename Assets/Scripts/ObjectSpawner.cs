using System.Collections;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public GameObject distractorPrefab; // Prefab for distractor objects
    public GameObject specialPrefab; // Prefab for the special object
    public float spawnInterval = 1f; // Time between spawning distractors
    public float spawnDistance = 10f; // Distance from the player to spawn objects
    public float spawnHeight = 5f; // Height above the terrain to spawn objects
    public float gameDuration = 100f; // Total game duration in seconds
    public float[] specialSpawnTimes = { 50f, 75f, 90f }; // Times to spawn special objects

    private void Start()
    {
        StartCoroutine(SpawnObjects());
    }

    private IEnumerator SpawnObjects()
    {
        float timer = 0f;

        while (timer < gameDuration && MagneticObject.winCondition==false)
        {
            // Check if it's time to spawn a special object
            bool specialSpawned = false;
            foreach (float spawnTime in specialSpawnTimes)
            {
                if (timer >= spawnTime)
                {
                    SpawnSpecial();
                    specialSpawned = true; // Mark that a special object has been spawned
                    // Remove the spawn time after spawning to avoid repeating
                    specialSpawnTimes = RemoveSpawnTime(specialSpawnTimes, spawnTime);
                    break; // Exit the loop to avoid spawning multiple specials in one iteration
                }
            }

            // Spawn a distractor object only if a special object wasn't spawned this interval
            if (!specialSpawned)
            {
                SpawnDistractor();
            }

            timer += spawnInterval; // Update the timer
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private void SpawnDistractor()
    {
        GameObject obj = Instantiate(distractorPrefab);
        obj.transform.position = transform.position + Vector3.forward * spawnDistance + Vector3.up * spawnHeight;
    }

    private void SpawnSpecial()
    {
        GameObject obj = Instantiate(specialPrefab);
        obj.transform.position = transform.position + Vector3.forward * spawnDistance + Vector3.up * spawnHeight;
    }

    private float[] RemoveSpawnTime(float[] spawnTimes, float spawnTime)
    {
        // Create a list to hold remaining spawn times
        System.Collections.Generic.List<float> updatedList = new System.Collections.Generic.List<float>();
        foreach (float time in spawnTimes)
        {
            if (time != spawnTime)
            {
                updatedList.Add(time);
            }
        }
        return updatedList.ToArray(); // Convert back to array
    }
}
