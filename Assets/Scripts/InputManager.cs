using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private PlayerInput playerInput; //Reference to PlayerInput Class in InputSystem Folder
    private PlayerInput.OnFootActions onFoot; //Reference to OnFoot action map

    private PlayerController playerController;
    private CameraController cameraController;

    void Awake()
    {
        playerInput = new PlayerInput();
        onFoot = playerInput.OnFoot;
        playerController = GetComponent<PlayerController>();
        cameraController = GetComponent<CameraController>();
        onFoot.Jump.performed += ctx => playerController.playerJump(); // Any time onFoot.Jump is performed we use call back contecxt (ctx) to call playerJump

        //onFoot.Crouch.performed += ctx => playerController.playerCrouch();
        //onFoot.Sprint.performed += ctx => playerController.playerSprint();
    }

    void Update()
    {
        //playerController.playerMovingForce(onFoot.Movement.ReadValue<Vector2>());
    }

    private void LateUpdate()
    {
        cameraController.PlayerLook(onFoot.Look.ReadValue<Vector2>());
    }

    private void OnEnable()
    {
        onFoot.Enable();
    }

    private void OnDisable()
    {
        onFoot.Disable();
    }
}
