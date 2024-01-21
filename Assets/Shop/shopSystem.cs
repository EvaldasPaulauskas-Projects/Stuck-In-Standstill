using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shopSystem : MonoBehaviour
{
    public Transform[] spawnPointsItems;
    public GameObject[] spawnedItems;

    public void updateShop()
    {
        // Shuffle the spawn points array
        List<Transform> shuffledSpawnPoints = new List<Transform>(spawnPointsItems);
        for (int i = 0; i < shuffledSpawnPoints.Count; i++)
        {
            int randomIndex = Random.Range(i, shuffledSpawnPoints.Count);
            Transform temp = shuffledSpawnPoints[i];
            shuffledSpawnPoints[i] = shuffledSpawnPoints[randomIndex];
            shuffledSpawnPoints[randomIndex] = temp;
        }

        // Iterate through all spawn points
        for (int i = 0; i < shuffledSpawnPoints.Count; i++)
        {
            // Randomly decide whether to spawn an item 
            if (Random.Range(0f, 1f) < 0.5f)
            {
                GameObject randomItem = getRandomItem();
                Instantiate(randomItem, shuffledSpawnPoints[i].position, Quaternion.identity);
            }
        }
    }

    public void clearShop()
    {
        // Find all GameObjects with the "buyableItem" layer
        GameObject[] buyableItems = GameObject.FindGameObjectsWithTag("buyableItem");

        // Delete each found GameObject
        foreach (GameObject buyableItem in buyableItems)
        {
            Destroy(buyableItem);
        }
    }

    private GameObject getRandomItem()
    {
        // Replace this with your actual logic for selecting a random item
        return spawnedItems[Random.Range(0, spawnedItems.Length)];
    }
}