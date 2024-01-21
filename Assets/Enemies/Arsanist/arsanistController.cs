using System.Collections;
using UnityEngine;

public class arsanistController : MonoBehaviour
{

    [Header("Arsanist Values/Objects")]
    private GameObject player;
    public Transform shootPoint;
    public GameObject bulletPrefab;

    public int maxBulletsSpawn;
    public float bulletSpeed = 5f;
    public int damage;

    [Header("Arsanist Animation")]
    private Animator arsanAnim;

    public AudioSource spawnBulletSound;

    void Start()
    {
        arsanAnim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");

        // Start the coroutine 
        StartCoroutine(SwitchStatesRandomly());
    }

    IEnumerator SwitchStatesRandomly()
    {
        while (true)
        {
            // Wait for a random time between 2 and 4 seconds
            float waitTime = Random.Range(2f, 4f);
            yield return new WaitForSeconds(waitTime);

            // Switch to attack state
            SwitchToAttackState();

            // Wait for the attack animation to finish
            float attackAnimationLength = arsanAnim.GetCurrentAnimatorStateInfo(0).length;
            yield return new WaitForSeconds(attackAnimationLength);

            // Switch back to idle state
            SwitchToIdleState();
        }
    }

    void SwitchToAttackState()
    {
        // Set the trigger parameter for the attack anim
        arsanAnim.SetTrigger("AttackTrigger");

        StartCoroutine(spawnBullet());
    }

    void SwitchToIdleState()
    {
        // Set the trigger parameter for the idle anim
        arsanAnim.SetTrigger("IdleTrigger");
    }

    IEnumerator spawnBullet()
    {
        spawnBulletSound.Play();
        yield return new WaitForSeconds(0.83f);

        // Spawn a random number of bullets
        int numberOfBullets = Random.Range(1, maxBulletsSpawn);

        for (int i = 0; i < numberOfBullets; i++)
        {
            // Calculate a random distance for each bullet 
            float randomDistance = Random.Range(-0.45f, 0.46f);

            // Spawn a bullet from the shootPoint with the calculated offset
            if (player != null && shootPoint != null)
            {
                Vector2 directionToPlayer = (player.transform.position - shootPoint.position).normalized;

                // Instantiate the bullet at the shootPoint position with the random distance offset
                Vector3 spawnPosition = shootPoint.position + new Vector3(randomDistance, 0f, 0f);
                GameObject bullet = Instantiate(bulletPrefab, spawnPosition, Quaternion.identity);

                bullet.GetComponent<arsanistBullet>().damage = damage;

                // Get the bullet's rigidbody and apply force in the right direction
                Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
                if (bulletRb != null)
                {
                    bulletRb.velocity = directionToPlayer * bulletSpeed;
                }
            }

            // Wait for a short delay between spawning bullets
            yield return new WaitForSeconds(0.2f);
        }
    }
}