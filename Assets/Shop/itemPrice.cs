using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ItemPrice : MonoBehaviour
{
    [Header("Items Values/Objects")]
    public GameObject itemUI;
    public TMP_Text itemPriceUI;
    [SerializeField] int totalItemPrice;

    public bool isInteracting;

    [Header("Player Values/Objects")]
    public playerController playerController;
    public itemHandler itemHandler;

    [Header("Item Type")]
    // Fast way for time crunch
    [SerializeField] bool healingItem;
    [SerializeField] bool ammoItem;
    [SerializeField] bool upgradeWeapon;
    [SerializeField] bool upgradeSword;

    private void Start()
    {
        // Find playerController
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<playerController>();

        // Find itemhandler
        itemHandler = GameObject.FindGameObjectWithTag("GunHolder").GetComponent<itemHandler>();

        // Determine price
        DeterminePrice();
    }

    private void Update()
    {
        // Check if player is interacting and can buy it
        if (Input.GetKeyDown(KeyCode.E) && isInteracting && playerController.moneyAmount >= totalItemPrice)
        {
            // Detuct player money
            playerController.moneyAmount -= totalItemPrice;

            // Determine what he bought
            if (healingItem)
            {
                healingItemFunctuon();
            }else if (ammoItem)
            {
                ammoItemFunctuon();
            }else if (upgradeWeapon)
            {
                upgradWeaponFunction();
            }else if(upgradeSword)
            {
                upgradeSwordFunction();
            }

            // Destroy object
            Destroy(gameObject);

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            itemUI.SetActive(true);
            isInteracting = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            itemUI.SetActive(false);
            isInteracting = false;
        }
    }

    public void DeterminePrice()
    {
        // Get items random price
        int randomRange = Random.Range(1, 11);
        int multiplyByWave = GameObject.FindGameObjectWithTag("WaveController").GetComponent<waveSystem>().currentWave;
        totalItemPrice = randomRange * multiplyByWave;

        // Display the random price
        itemPriceUI.text = totalItemPrice.ToString();
    }

    
    public void healingItemFunctuon()
    {
        playerController.healthAmount += 15;
    }

    public void ammoItemFunctuon()
    {
        itemHandler.givePistolAmmo(15);
    }

    public void upgradWeaponFunction()
    {
        itemHandler.increaseWeaponDamage(7);
    }

    public void upgradeSwordFunction()
    {
        itemHandler.increaseSwordDamage(9);
    }
}