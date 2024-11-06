using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;

    public float speed = 12f;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    Vector3 playerVelocity;
    bool isGrounded;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        ProcessGravity();

        ProcessMovement();
    }

    void ProcessMovement()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 movement = transform.right * moveX + transform.forward * moveZ;

        controller.Move(speed * Time.deltaTime * movement);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            playerVelocity.y = Mathf.Sqrt(jumpHeight * -2.0f * gravity);
        }
    }

    void ProcessGravity()
    {
        if (isGrounded && playerVelocity.y < 0.0f)
        {
            playerVelocity.y = -2.0f;
        }
        else
        {
            playerVelocity.y += gravity * Time.deltaTime;
        }

        controller.Move(playerVelocity * Time.deltaTime);
    }
}
