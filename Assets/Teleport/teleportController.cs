using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class teleportController : MonoBehaviour
{
    [SerializeField] Transform teleportPos;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            collision.gameObject.transform.position = teleportPos.transform.position;
        }
    }
}
