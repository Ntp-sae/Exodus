using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public InputReader reader;

    private CharacterController playerController;
    private float playerVelocity;
    public float speed; //10f
    public float walkSpeed; //10f
    public float crouchSpeed; //5f
    public float sprintSpeed; //15f

    Vector3 moveDirection;

    public float _gravity;
    public float _gravityMultiplier;
    private bool isGrounded;

    public float jumpHeight;
    private bool lerpCrouch = false;
    private bool isCrouching = false;
    public float crouchTimer = 1f;

    private bool isMovig = false;
    private bool isSprinting = false;

    private Vector3 playerMovement;

    void Start()
    {
        playerController = GetComponent<CharacterController>();
        speed = walkSpeed;
        //_gravity = -10f;
        //_gravityMultiplier = 1f;
        //jumpHeight = 3f;
    }

    void OnEnable()
    {
        reader.movementEvent += movementInput;
        reader.crouchEvent += playerCrouch;
        reader.sprintPressEvent += playerStartSprint;
        reader.sprintReleaseEvent += playerStopSprint;
        reader.jumpEvent += playerJump;
    }

    void OnDisable()
    {
        reader.movementEvent -= movementInput;
        reader.crouchEvent -= playerCrouch;
        reader.sprintPressEvent -= playerStartSprint;
        reader.sprintReleaseEvent -= playerStopSprint;
        reader.jumpEvent -= playerJump;
    }

    void Update()
    {
        gravity();
        playerMovingForce();

        if (lerpCrouch)
        {
            crouchTimer += Time.deltaTime;
            float p = crouchTimer / 1;
            p *= p;
            if (isCrouching)
            {
                playerController.height = Mathf.Lerp(playerController.height, 1, p);
                speed = crouchSpeed;
            }
            else
            {
                playerController.height = Mathf.Lerp(playerController.height, 2, p);
                speed = walkSpeed;
            }

            if (p > 1)
            {
                lerpCrouch = false;
                crouchTimer = 0f;
            }
        }
    }

    private void movementInput(Vector3 input)
    {
        moveDirection = input;
    }

    public void playerMovingForce()
    {
        playerMovement = (transform.forward * moveDirection.z + transform.right * moveDirection.x);
        playerMovement.y = playerVelocity;
        playerController.Move(playerMovement * speed * Time.deltaTime);
    }

    public void playerJump()
    {
        if (playerController.isGrounded)
        {
            playerVelocity = jumpHeight;
            Debug.Log("ImJumping");
        }
    }

    public void playerCrouch()
    {
        isCrouching = !isCrouching;
        crouchTimer = 0;
        lerpCrouch = true;
    }

    public void playerStartSprint()
    {
        speed = sprintSpeed;
    }

    public void playerStopSprint()
    {
        speed = walkSpeed;
    }

    private void gravity()
    {
        if (playerController.isGrounded && playerVelocity < 0.0f)
        {
            playerVelocity = -1.0f;
        }
        else
        {
            playerVelocity += _gravity * _gravityMultiplier * Time.deltaTime;
        }
    }
}
