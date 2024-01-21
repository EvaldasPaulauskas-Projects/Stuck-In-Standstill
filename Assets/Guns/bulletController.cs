using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletController : MonoBehaviour
{
    playerController playerControll;

    public int bulletDamage;

    private void Start()
    {
        // Find player Controller
        playerControll = GameObject.FindGameObjectWithTag("Player").GetComponent<playerController>();
    }

    private void Update()
    {
        playerControll.playerSpeed = 5;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        playerControll.playerSpeed = 0;

        // Check for wall collision
        if (collision.gameObject.layer == 6)
        {
            Destroy(gameObject);
        }

        // Check for enemy collision
        if (collision.gameObject.layer == 7)
        {
            // Get the direction from the bullet to the enemy for knockback
            Vector2 knockbackDirection = collision.transform.position - transform.position;
            knockbackDirection.Normalize(); // Normalize the direction

            collision.gameObject.GetComponent<enemyController>().damageTaken(bulletDamage, knockbackDirection);
            Destroy(gameObject);
        }
    }
}
