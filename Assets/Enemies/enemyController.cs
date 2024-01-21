using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyController : MonoBehaviour
{
    [Header("Enemy Objects/Values")]
    public int health;
    public float knockbackForce = 5f;

    public AudioSource hitEffect;
    Rigidbody2D rb;

    [Header("Enemy Abilities/Attacks")]
    public int baseDamage;
    public int baseSpeed;

    public bool canMove;

    [Header("Player Object")]
    GameObject playerObject;

    [Header("Distance Limit")]
    public float distanceLimit = 2.0f;

    [Header("Dropped Items")]
    public GameObject[] droppedItems; 

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerObject = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        if (health <= 0)
        {
            // Decrease enemy
            GameObject.FindGameObjectWithTag("WaveController").GetComponent<waveSystem>().ammountOfEnemiesLeft -= 1;
            dropItems();
            Destroy(gameObject);
        }

        // Move towards player within distance limit
        if (canMove)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, playerObject.transform.position);

            if (distanceToPlayer > distanceLimit)
            {
                transform.position = Vector3.MoveTowards(transform.position, playerObject.transform.position, baseSpeed * Time.deltaTime);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            // Get the direction from the bullet to the enemy for knockback
            Vector2 knockbackDirection = collision.transform.position - transform.position;
            knockbackDirection.Normalize(); // Normalize the direction

            collision.gameObject.GetComponent<playerController>().damageTaken(25, knockbackDirection);
        }
    }

    // Damage taken void
    public void damageTaken(int damage, Vector2 knockbackDirection)
    {
        health -= damage;
        hitEffect.Play();
        ApplyKnockback(knockbackDirection);
    }

    // Knockback
    void ApplyKnockback(Vector2 knockbackDirection)
    {
        // Apply the force to the rigidbody
        if (rb != null)
        {
            rb.AddForce(knockbackDirection * knockbackForce, ForceMode2D.Impulse);
            StartCoroutine(knockbackDisabled());
        }
    }

    IEnumerator knockbackDisabled()
    {
        yield return new WaitForSeconds(0.25f);
        rb.velocity = Vector3.zero;
    }

    void dropItems()
    {
        // Check if there are items in the array
        if (droppedItems.Length > 0)
        {
            // Generate a random index (0 or 1)
            int randomIndex = Random.Range(0, 2);

            // Instantiate the randomly chosen item at the transform location
            GameObject itemToInstantiate = droppedItems[randomIndex];
            Instantiate(itemToInstantiate, transform.position, Quaternion.identity);
        }
        else
        {
            Debug.LogWarning("No items in the droppedItems array.");
        }
    }
}