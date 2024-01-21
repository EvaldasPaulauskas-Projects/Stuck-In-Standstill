using System.Collections;
using TMPro;
using UnityEngine;

public class playerController : MonoBehaviour
{
    [Header("Player Objects/Values")]
    public float playerSpeed;

    public int healthAmount;
    public int moneyAmount;

    public float knockbackForce;

    bool knockBack;

    AudioSource hitAudio;

    [Header("Ammout Display/UI")]
    public TMP_Text healthText;
    public TMP_Text moneyText;

    private Rigidbody2D rigbodyPlayer;
    private Vector2 movement;

    [Header("Player Animation States")]
    public Animator playerAnim;

    public bool ismMoving;

    [Header("Hand Objects/Values")]
    public GameObject handRot;
    public SpriteRenderer playerSprite;

    [Header("PlayerGunHolder Objects/Values")]
    public GameObject playerGunHolder;

    [Header("Player Death")]
    public DeathController deathController;

    private void Start()
    {
        // Get player rigbody
        rigbodyPlayer = GetComponent<Rigidbody2D>();

        hitAudio = GetComponent<AudioSource>();
    }

    private void Update()
    {
        // Get axis
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        // Mouse Position
        Vector3 mousePos = Input.mousePosition;

        // Object position
        Vector3 objectPos = Camera.main.WorldToScreenPoint(transform.position);
        mousePos.x = mousePos.x - objectPos.x;
        mousePos.y = mousePos.y - objectPos.y;

        // Angle calculation
        float angle = Mathf.Atan2(-mousePos.y, -mousePos.x) * Mathf.Rad2Deg;
        handRot.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle ));

        // Set spriteRenderer rotation
        if (angle > 90f || angle < -90f)
        {
            playerSprite.flipX = true;
        }
        else{
            playerSprite.flipX = false;
        }

        // Display health/money
        healthText.text = healthAmount.ToString();
        moneyText.text = moneyAmount.ToString();

        // Health logic
        if(healthAmount <= 0)
        {
            deathController.enabled = true;
        }
    }

    private void FixedUpdate()
    {
        // Check if player is moving
        if(playerSpeed != 0)
        {
            if (movement.x != 0 || movement.y != 0) { ismMoving = true; } else { ismMoving = false; }
        }
        else { ismMoving = false; }

        // Player animation state
        switch (ismMoving)
        {
            case true:
                playerAnim.SetBool("isMoving", true);
                break;
            case false:
                playerAnim.SetBool("isMoving", false);
                break;
        }

        if (ismMoving)
        {
            playerAnim.SetFloat("PlayerWalkState", movement.x);
        }

        // Normalze player movement
        if(!knockBack)
        {
            Vector2 direction = movement.normalized;
            rigbodyPlayer.MovePosition(rigbodyPlayer.position + direction * playerSpeed * Time.fixedDeltaTime);
        }
    }

    public void damageTaken(int damage, Vector2 knockbackDirection)
    {
        healthAmount -= damage;
        hitAudio.Play();
        ApplyKnockback(knockbackDirection);
        knockBack = true;
    }

    void ApplyKnockback(Vector2 knockbackDirection)
    {
        // Apply the force to the rigidbody
        if (rigbodyPlayer != null)
        {
            rigbodyPlayer.AddForce(knockbackDirection * knockbackForce, ForceMode2D.Impulse);

            StartCoroutine(knockbackDisabled());
        }
    }

    IEnumerator knockbackDisabled()
    {
        yield return new WaitForSeconds(0.15f);
        rigbodyPlayer.velocity = Vector3.zero;
        knockBack = false;
    }
}
