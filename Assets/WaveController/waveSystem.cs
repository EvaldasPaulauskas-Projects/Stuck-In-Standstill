using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class waveSystem : MonoBehaviour
{
    [Header("Enemy Objects/Values")]
    public Transform[] spawnPoints;
    public GameObject[] enemyObjects;

    [Header("Wave System Objects/Values")]
    public int currentWave;
    int increaseEnemyHealth = 12;

    public int ammountOFEnemies;
    public int ammountOfEnemiesLeft;
    public bool waveStarted;

    [Header("Wave System Text")]
    public TMP_Text waveSystemText;

    [Header("Keeper Objects/Values")]
    public Animator keeperAnim;
    public GameObject keeperObject;

    public AudioSource keeperAudio;

    [Header("Arena Objects/Values")]
    public Animator arenaAnim;

    [Header("Weapon Controller")]
    public weaponController weaponController;

    [Header("Shop System")]
    public shopSystem shopSystem;

    private void Update()
    {
        // Track waves in UI
        waveSystemText.text = "Ready To Start Wave " + currentWave + " ?";
        // Disable Wave Spawning
        if (ammountOfEnemiesLeft == 0 && waveStarted)
        {
            // Disable wave system
            waveStarted = false;
            weaponController.isInf = true;
            // Spawn Keeper
            StartCoroutine(spawnInKeeper());
        }
    }

    // Start wave system
    public void startWaveSystem(GameObject UI)
    {
        UI.SetActive(false);

        keeperAnim.SetBool("keeperActive", false);
        keeperAnim.SetBool("keeperDied", true);

        weaponController.isInf = false;

        StartCoroutine(despawnKeeper());
    }

    public void closeUI(GameObject UI)
    {
        UI.SetActive(false);
    }

    // Despawn keeper and start spawning enemies
    IEnumerator despawnKeeper()
    {
        arenaAnim.SetBool("isWaveSpawning", true);
        keeperAudio.Play();
        yield return new WaitForSeconds(4f);
        keeperObject.SetActive(false);

        waveStarted = true;

        // Wave Randomizer
        currentWave++;
        int tempRandom = Random.Range(0, 5);
        ammountOFEnemies = ammountOFEnemies + currentWave + tempRandom;
        ammountOfEnemiesLeft = ammountOFEnemies;

        // Spawn enemies
        StartCoroutine(SpawnEnemies());
    }

    IEnumerator spawnInKeeper()
    {
        // Clear the shop
        shopSystem.clearShop();

        keeperObject.SetActive(true);
        arenaAnim.SetBool("isWaveSpawning", false);
        keeperAnim.SetBool("keeperAlive", true);

        yield return new WaitForSeconds(5f);

        // Update the shop
        shopSystem.updateShop();

        keeperAnim.SetBool("keeperIdle", true);

        keeperAnim.SetBool("keeperDied", false);
        keeperAnim.SetBool("keeperAlive", false);
    }

    // Spawn enemies coroutine
    IEnumerator SpawnEnemies()
    {
        for (int i = 0; i < ammountOFEnemies; i++)
        {
            // Pick a random spawn point and enemy type
            Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
            GameObject enemyPrefab = enemyObjects[Random.Range(0, enemyObjects.Length)];

            // Instantiate the enemy at the chosen spawn point
            GameObject spawnedEnemy = Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);

            int tempIncreaseRange = Random.Range(0, 9);
            int waveIncrease = tempIncreaseRange * currentWave;

            spawnedEnemy.GetComponent<enemyController>().health += increaseEnemyHealth + waveIncrease;

            // Wait for 2 seconds before spawning the next enemy
            yield return new WaitForSeconds(3f);
        }
    }
}
