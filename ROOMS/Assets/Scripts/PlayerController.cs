using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Player Movements")]
    public float walkSpeed = 5f;
    public float runSpeed = 8f;
    public bool isRunning;
    public float jumpHeight = 3f;
    public CharacterController controller;


    [Header("Gravity Variables")]
    public float gravity = -9.81f;
    public float groundDistance = 0.4f;
    public bool isGrounded;
    public Transform groundCheck;
    public LayerMask groundMask;
    private Vector3 playerVelocity = Vector3.zero;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        HandleJumping();
        HandleGravity();
        HandleMovement();
    }

    void HandleMovement()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveZ = Input.GetAxisRaw("Vertical");
        isRunning = Input.GetButton("Run");

        Vector3 moveDirection = transform.right * moveX + transform.forward * moveZ;
        moveDirection = moveDirection.normalized;

        controller.Move((isRunning ? runSpeed : walkSpeed) * Time.deltaTime * moveDirection);
    }

    void HandleGravity()
    {
        if (isGrounded && playerVelocity.y < 0f)
        {
            playerVelocity.y = -2f;
        }
        else
        {
            playerVelocity.y += gravity * Time.deltaTime;
        }

        controller.Move(playerVelocity * Time.deltaTime);
    }

    void HandleJumping()
    {
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
    }
}
