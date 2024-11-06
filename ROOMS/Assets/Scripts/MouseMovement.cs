using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseMovement : MonoBehaviour
{
    public float mouseSensitivity = 500f;

    public Transform playerBody;

    float xRotation = 0f;

    public float topClamp = -90f;
    
    public float bottomClamp = 90f;

    // Start is called before the first frame update
    void Start()
    {
        // Locking the cursor to the middle of the screen and making it invisible
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        // Getting the mouse inputs
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // Rotation around the x axis (Look up and down)
        xRotation -= mouseY;

        // Clamp the rotation
        xRotation = Mathf.Clamp(xRotation, topClamp, bottomClamp);

        // Apply rotations to our transform
        playerBody.Rotate(Vector3.up * mouseX);
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
    }
}
