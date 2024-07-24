using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public InputReader reader;

    private CharacterController playerController;
    [SerializeField] Animator PlayerAnimator;
    private float playerVelocity;
    public float speed; //10f
    public float walkSpeed; //10f
    public float crouchSpeed; //5f
    public float sprintSpeed; //15f

    Vector3 moveDirection;

    public float _gravity;
    public float _gravityMultiplier;
    private bool isGrounded;
    private bool isSprinting;

    public float jumpHeight;
    private bool lerpCrouch = false;
    private bool isCrouching = false;
    public float crouchTimer = 1f;

    private Vector3 playerMovement;
    private Vector2 mouseMovement;

    //----------------------------------------------------//

    public Camera cam;
    private float xRotation = 0f;
    private float yRotation = 0f;
    public float xSensitivity = 10f;
    public float ySensitivity = 10f;

    public float xRotationTopClamp = -90f;
    public float xRotationBotClamp = 90f;

    float mouseX;
    float mouseY;

    void Start()
    {
        playerController = GetComponent<CharacterController>();
        speed = walkSpeed;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void OnEnable()
    {
        reader.movementEvent += movementInput;
        reader.crouchEvent += playerCrouch;
        reader.sprintPressEvent += playerStartSprint;
        reader.sprintReleaseEvent += playerStopSprint;
        reader.jumpEvent += playerJump;
        reader.mouseEvent += mouseInput;
    }

    void OnDisable()
    {
        reader.movementEvent -= movementInput;
        reader.crouchEvent -= playerCrouch;
        reader.sprintPressEvent -= playerStartSprint;
        reader.sprintReleaseEvent -= playerStopSprint;
        reader.jumpEvent -= playerJump;
        reader.mouseEvent -= mouseInput;
    }

    void Update()
    {
        MouseInput();
        playerMovingForce();
        gravity();
        animationPlayer();

        Debug.Log(playerController.velocity.z);

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

    private void MouseInput()
    {
        // Gett the mouse input
        mouseX = mouseMovement.x * xSensitivity * Time.deltaTime;
        mouseY = mouseMovement.y * ySensitivity * Time.deltaTime;

        //X axis rotation
        xRotation -= mouseY;

        //Clamp rotation
        xRotation = Mathf.Clamp(xRotation, xRotationTopClamp, xRotationBotClamp);

        //Y axis rotation
        yRotation += mouseX;

        //Apply rotations to transforms (Camera and player)
        transform.rotation = Quaternion.Euler(0f, yRotation, 0f);
        cam.transform.rotation = Quaternion.Euler(xRotation, yRotation, 0f);
    }

    private void movementInput(Vector3 input)
    {
        moveDirection = input;
    }

    public void playerMovingForce()
    {
        //Assign Velocity
        playerMovement = (transform.forward * moveDirection.z + transform.right * moveDirection.x);
        playerMovement.y = playerVelocity;
        playerController.Move(playerMovement * speed * Time.deltaTime);
    }

    public void playerJump()
    {
        if (playerController.isGrounded)
        {
            playerVelocity = jumpHeight;
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
        //Assign Speed
        speed = sprintSpeed;
        isSprinting = true;
    }

    public void playerStopSprint()
    {
        //Assign Speed
        speed = walkSpeed;
        isSprinting = false;
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
    private void mouseInput(Vector2 input)
    {
        mouseMovement = input;
    }

    private void animationPlayer()
    {
        //Assign Animation
        if (playerController.velocity.z < 0f)
        {
            PlayerAnimator.SetBool("isWalking", true);
            PlayerAnimator.SetBool("isSprinting", false);
            PlayerAnimator.SetBool("isIdle", false);
        }
        else if (playerController.velocity.z > 0.1f)
        {
            PlayerAnimator.SetBool("isWalking", true);
            PlayerAnimator.SetBool("isSprinting", false);
            PlayerAnimator.SetBool("isIdle", false);
        }
        else if (playerController.velocity.z <= 0.1f)
        {
            PlayerAnimator.SetBool("isWalking", false);
            PlayerAnimator.SetBool("isSprinting", false);
            PlayerAnimator.SetBool("isIdle", true);
        }
        if (isSprinting == true)
        {
            PlayerAnimator.SetBool("isWalking", false);
            PlayerAnimator.SetBool("isSprinting", true);
            PlayerAnimator.SetBool("isIdle", false);
        }
    }
}