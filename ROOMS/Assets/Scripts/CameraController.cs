using System.Collections;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float mouseSensitivity = 500f;

    float xRotation = 0f;

    public float topClamp = -90f;

    public float bottomClamp = 90f;

    public Transform body;
    public Transform arms;
    // public Transform leftArm;
    // public Transform rightArm;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        HandleMouseLook();
    }

    void HandleMouseLook()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;

        xRotation = Mathf.Clamp(xRotation, topClamp, bottomClamp);

        // leftArm.localRotation = Quaternion.Euler(new Vector3(xRotation, 0, 0));
        // rightArm.localRotation = Quaternion.Euler(new Vector3(xRotation, 0, 0));
        arms.localRotation = Quaternion.Euler(new Vector3(xRotation, 0, 0));
        body.Rotate(new Vector3(0, mouseX, 0));
        // transform.parent.Rotate(Vector3.up * mouseX);
        // transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
    }
}
