using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class arsanistBullet : MonoBehaviour
{
    public int damage;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            // Get the direction from the bullet to the enemy for knockback
            Vector2 knockbackDirection = collision.transform.position - transform.position;
            knockbackDirection.Normalize(); // Normalize the direction

            collision.gameObject.GetComponent<playerController>().damageTaken(damage, knockbackDirection);

            Destroy(gameObject);
        }

        Destroy(gameObject);
    }
}
