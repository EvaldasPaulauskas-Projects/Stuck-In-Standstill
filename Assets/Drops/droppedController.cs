using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class droppedController : MonoBehaviour
{
    public bool isMoney;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        int randomNumber = Random.Range(1, 9);
        int total = randomNumber * GameObject.FindGameObjectWithTag("WaveController").GetComponent<waveSystem>().currentWave;
        if(collision.gameObject.tag == "Player")
        {
            if (isMoney)
            {
                collision.gameObject.GetComponent<playerController>().moneyAmount += total;

                Destroy(gameObject);
            }
            else if(collision.gameObject.tag == "Player")
            {
                GameObject weaponObject = GameObject.FindGameObjectWithTag("GunHolder");
                weaponObject.GetComponent<itemHandler>().givePistolAmmo(total);

                Destroy(gameObject);
            }
        }
    }
}
