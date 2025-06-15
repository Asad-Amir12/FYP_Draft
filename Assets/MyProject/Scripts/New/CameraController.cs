using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float sensitivity = 100f; // Sensitivity for mouse movement
    [SerializeField] private float smoothTime = 0.1f; // Smoothness of the rotation
    private Transform player;
    private float smoothMouseX; // Smoothed mouse input
    private float velocityX; // Velocity for smoothing

    void Start()
    {
        player = transform.parent; // Assuming the camera is a child of the player object
        Cursor.lockState = CursorLockMode.Locked; // Lock the cursor to the center of the screen
    }

    void Update()
    {
        RotateCamera();
    }

    private void RotateCamera()
    {
        // Get raw mouse input
        float mouseX = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        //float mouseY = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;

        // Smooth the mouse input
        smoothMouseX = Mathf.SmoothDamp(smoothMouseX, mouseX, ref velocityX, smoothTime);
        //smoothMouseY = Mathf.SmoothDamp(smoothMouseY, mouseY, ref velocityY, smoothTime);

        // Rotate the player horizontally
        player.Rotate(Vector3.up * smoothMouseX);

        // Rotate the camera vertically
        //xRotation -= smoothMouseY;
        //xRotation = Mathf.Clamp(xRotation, -90f, 90f); // Clamp vertical rotation to prevent flipping
        //transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
    }
}