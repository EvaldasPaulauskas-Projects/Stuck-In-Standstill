using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathController : MonoBehaviour
{
    [Header("Player Objects/Values")]
    public playerController controller;
    public GameObject hamdRot;

    [Header("Player Death")]
    public Animator playerAnim;
    public Animator deathAnim;
    private bool hasPlayedDeathAnimation = false;

    [Header("Player UI")]
    public GameObject DeathUI;
    public GameObject PlayerUI;

    public waveSystem waveSystem;

    public TMP_Text waveText;

    private void Start()
    {
        controller = GetComponent<playerController>();
        playerAnim = GetComponent<Animator>();
    }

    private void Update()
    {
        // Check if the player is not dead and the death animation hasn't been played
        if (controller.enabled && !hasPlayedDeathAnimation)
        {
            waveSystem.enabled = false;

            waveText.text = "Waves Survived : " + waveSystem.currentWave.ToString();

            // Set the player to dead state
            controller.enabled = false;
            playerAnim.SetTrigger("playerIsDead");
            hamdRot.SetActive(false);

            // Set the flag to true to avoid playing the death animation again
            hasPlayedDeathAnimation = true;

            // Destroy all objects with the specified layer
            DestroyEnemies();

            StartCoroutine(deathWait());
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    private void DestroyEnemies()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject enemy in enemies)
        {
            Destroy(enemy);
        }
    }

    IEnumerator deathWait()
    {
        PlayerUI.SetActive(false);
        yield return new WaitForSeconds(2f);
        DeathUI.SetActive(true);

        yield return new WaitForSeconds(3f);
        deathAnim.SetTrigger("deathIsFinished");
    }
}