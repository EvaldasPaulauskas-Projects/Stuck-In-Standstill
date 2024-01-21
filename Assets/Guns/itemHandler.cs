using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemHandler : MonoBehaviour
{
    [Header("Weapon1 Object/Assets")]
    public GameObject weaponObject;
    public GameObject weaponUIObject;

    public weaponController weaponController;

    [Header("Weapon2 Object/Assets")]
    public GameObject swordObject;
    public GameObject swordUIObject;

    public swordWeaponController swordController;

    void Update()
    {
        // Check for scroll wheel movement
        float scrollWheel = Input.GetAxis("Mouse ScrollWheel");
        if (scrollWheel > 0f) // Scroll Up
        {
            ActivateWeapon1();
        }
        else if (scrollWheel < 0f) // Scroll Down
        {
            ActivateWeapon2();
        }

        // Check for key presses
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ActivateWeapon1();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ActivateWeapon2();
        }
    }

    void ActivateWeapon1()
    {
        weaponObject.SetActive(true);
        weaponUIObject.SetActive(true);

        swordObject.SetActive(false);
        swordUIObject.SetActive(false);
    }

    void ActivateWeapon2()
    {
        weaponObject.SetActive(false);
        weaponUIObject.SetActive(false);

        swordObject.SetActive(true);
        swordUIObject.SetActive(true);
    }

    // Give pistol ammo
    public void givePistolAmmo(int totalAmmount)
    {
        weaponController.getAmmo(totalAmmount);
    }

    // Increase weapon damage
    public void increaseWeaponDamage(int damageInc)
    {
        weaponController.incgetDamage(damageInc);
    }

    // Increase sword damage
    public void increaseSwordDamage(int damageSwordInc)
    {
        swordController.incgetSwordDamage(damageSwordInc);
    }
}