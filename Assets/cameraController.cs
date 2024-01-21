using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraController : MonoBehaviour
{
    private GameObject playerObject;
    public Vector3 cameraOffset;

    public float maxDistance = 2f;

    private void Start()
    {
        // FInd player object
        playerObject = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        transform.position = playerObject.transform.position + cameraOffset;

        // Get mouse position in world coordinates
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = -10f; // Ensure the mouse position is in the same Z plane as the player

        // Calculate the offset from player to mouse position
        Vector3 offsetToMouse = mousePosition - playerObject.transform.position;

        // Limit the offset to a maximum distance
        offsetToMouse = Vector3.ClampMagnitude(offsetToMouse, maxDistance);

        // Set the camera position based on the player and offset
        transform.position = playerObject.transform.position + offsetToMouse;
    }

}
