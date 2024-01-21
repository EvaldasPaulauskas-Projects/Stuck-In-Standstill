using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class swordWeaponController : MonoBehaviour
{
    [Header("Player")]
    [SerializeField]playerController playerControll;

    [Header("Sword Objects/Values")]
    private bool isSwinging = false;
    private bool isIdle = true;

    public float knockbackMultiplier = 1;
    public float knockbackStrength = 5;
    public int baseDamage = 15;

    public float swingCooldown = 0.45f;
    private float currentCooldown = 0;

    [Header("Sword Animation")]
    public Animator swordAnim;
    [SerializeField] AudioSource swordSwing;

    private void Start()
    {
        playerControll = GameObject.FindGameObjectWithTag("Player").GetComponent<playerController>();
    }

    void Update()
    {
        if (!isSwinging && isIdle)
        {
            if (currentCooldown <= 0 && Input.GetMouseButtonDown(0))
            {
                swordAnim.SetTrigger("isSwinging");
                isSwinging = true;
                playerControll.playerSpeed = 8;

                isIdle = false;
                currentCooldown = swingCooldown;

                swordSwing.Play();
            }
        }

        // Update the cooldown timer
        if (currentCooldown > 0)
        {
            currentCooldown -= Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isSwinging && collision.gameObject.layer == 7)
        {
            Vector2 knockbackDirection = collision.transform.position - transform.position;
            knockbackDirection.Normalize();

            Vector2 finalKnockback = knockbackDirection * knockbackStrength * knockbackMultiplier;

            collision.gameObject.GetComponent<enemyController>().damageTaken(baseDamage, finalKnockback);
        }
    }

    // Method to be called from animation event
    public void OnSwingAnimationEnd()
    {
        isSwinging = false;
        isIdle = true;
        playerControll.playerSpeed = 0;
        swordAnim.SetTrigger("isIdle");
    }

    public void OnswordIdle()
    {
        isSwinging = false;
        isIdle = true;
    }

    public void incgetSwordDamage(int ammount)
    {
        baseDamage += ammount;
    }
}