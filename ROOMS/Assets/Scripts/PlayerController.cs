using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Player Movements")]
    public float moveSpeed;
    public float walkSpeed = 5f;
    public float runSpeed = 8f;
    public bool isRunning;
    public float jumpHeight = 3f;
    public CharacterController controller;

    [Header("Speed Boost Variables")]
    private bool isSpeedBoostActive = false;
    private float speedMultiplier = 1f;
    private PlayerHUD playerHUD;


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
        moveSpeed = walkSpeed;


        playerHUD = FindObjectOfType<PlayerHUD>();
    }

    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        HandleMovement();
        HandleJumping();
        HandleGravity();
    }

    void HandleMovement()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveZ = Input.GetAxisRaw("Vertical");

        isRunning = Input.GetKey(KeyCode.LeftShift);
        Vector3 moveDirection = new Vector3(moveX, 0f, moveZ).normalized;
        moveDirection = transform.TransformDirection(moveDirection);
        moveSpeed = isRunning ? runSpeed * speedMultiplier : walkSpeed * speedMultiplier;

        Vector3 movement = moveSpeed * Time.deltaTime * new Vector3(moveDirection.x, 0, moveDirection.z);
        controller.Move(movement);
    }

    void HandleJumping()
    {
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            playerVelocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
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

        controller.Move(new Vector3(0, playerVelocity.y, 0) * Time.deltaTime);
    }

    // Speed Boost Methods
    public void ApplySpeedBoost(float multiplier, float duration)
    {
        if (!isSpeedBoostActive) 
        {
            StartCoroutine(SpeedBoostCoroutine(multiplier, duration));
        }
    }

    private IEnumerator SpeedBoostCoroutine(float multiplier, float duration)
    {
        isSpeedBoostActive = true;
        speedMultiplier = multiplier;

        // Notify the HUD
        if (playerHUD != null)
        {
            playerHUD.ShowSpeedBoost(true);
        }

        Debug.Log("Speed Boost Activated!");

        yield return new WaitForSeconds(duration);

        RemoveSpeedBoost();
    }

    public void RemoveSpeedBoost()
    {
        speedMultiplier = 1f;
        isSpeedBoostActive = false;

        // Notify the HUD
        if (playerHUD != null)
        {
            playerHUD.ShowSpeedBoost(false);
        }

        Debug.Log("Speed Boost Ended!");
    }
}
