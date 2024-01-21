using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waveController : MonoBehaviour
{
    [Header("WAVE UI")]
    public GameObject waveUI;
    bool entered;
    bool isWaveUIActive = false; // Added flag to track waveUI state
    [Header("Keeper")]
    public Animator keeperAnimator;

    private void Update()
    {
        // Toggle UI
        if (Input.GetKeyDown(KeyCode.E) && entered)
        {
            isWaveUIActive = !isWaveUIActive;
            waveUI.SetActive(isWaveUIActive);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            keeperAnimator.SetBool("keeperActive", true);
            entered = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            keeperAnimator.SetBool("keeperActive", false);
            entered = false;

            // Close UI only if it's currently active
            if (isWaveUIActive)
            {
                waveUI.SetActive(false);
                isWaveUIActive = false;
            }
        }
    }
}