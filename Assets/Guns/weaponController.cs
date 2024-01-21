using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class weaponController : MonoBehaviour
{
    [Header("Gun Values/Objects")]
    public int ammo;
    public int ammoCaptivity;

    public int bulletDamageWeapon;
    [SerializeField]int maxAmmoPerClip = 24;

    public float bulletSpeed;
    public bool isInf;

    [Header("Ammo DIsplay/UI")]
    public TMP_Text ammoText;

    public GameObject shootPointObject;
    public GameObject bulletPrefab;

    [Header("Gun Effects")]
    [SerializeField] AudioSource bulletSound;
    [SerializeField] AudioSource gunEmptySound;
    [SerializeField] AudioSource gunReloadSound;

    private void Update()
    {
        // Display ammo 
        ammoText.text = ammo.ToString() + " / " + ammoCaptivity.ToString();

        // Check if player has shot
        if (Input.GetMouseButtonDown(0) && ammo > 0)
        {
            // Remove ammo per shot
            if (!isInf) { ammo--; }

            // Instantiate the bullet
            GameObject bulletTemp = Instantiate(bulletPrefab, shootPointObject.transform.position, shootPointObject.transform.rotation);
            bulletTemp.GetComponent<bulletController>().bulletDamage = bulletDamageWeapon;
            // Get the rigidbody component of the bullet
            Rigidbody2D bulletRb = bulletTemp.GetComponent<Rigidbody2D>();

            // Check if the bullet has a rigidbody component
            if (bulletRb != null)
            {
                bulletRb.AddForce(shootPointObject.transform.up * bulletSpeed, ForceMode2D.Impulse);

                bulletSound.Play();
            }
        }

        // Check if gun is empty
        if(ammo <= 0 && Input.GetMouseButtonDown(0))
        {
            gunEmptySound.Play();
        }
        // Check if player is reloading
        if (Input.GetKeyDown(KeyCode.R))
        {
            Reload();
        }
    }

    public void Reload()
    {
        int tempFill = maxAmmoPerClip - ammo;

        // Check if there is enough ammoCaptivity to reload
        if (ammoCaptivity > 0)
        {
            if (ammoCaptivity >= tempFill)
            {
                ammoCaptivity -= tempFill;
                ammo = maxAmmoPerClip;
            }
            else
            {
                // If there is not enough ammoCaptivity to fill the clip, use all remaining ammoCaptivity
                ammo += ammoCaptivity;
                ammoCaptivity = 0;
            }

            gunReloadSound.Play();
        }
        else
        {
            gunEmptySound.Play();
        }
    }

    public void getAmmo(int ammo)
    {
        ammoCaptivity += ammo;
    }

    public void incgetDamage(int ammount)
    {
        bulletDamageWeapon += ammount;
    }
}
