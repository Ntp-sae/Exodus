using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(menuName = "Input Reader", fileName = "New Imput Reader")]
public class InputReader : ScriptableObject, PlayerInput.IOnFootActions
{
    public event Action<Vector3> movementEvent;
    public event Action<Vector2> mouseEvent;
    public event Action jumpEvent;
    public event Action crouchEvent;
    public event Action sprintPressEvent;
    public event Action sprintReleaseEvent;
    public event Action reloadEvent;
    public event Action shootPressEvent;
    public event Action shootReleaseEvent;
    public event Action weaponFirstPressEvent;
    public event Action weaponSecondPressEvent;

    private PlayerInput _playerInput;

    private void OnEnable()
    {
        if (_playerInput == null)
        {
            _playerInput = new PlayerInput();
            _playerInput.OnFoot.SetCallbacks(this);
            _playerInput.OnFoot.Enable();
        }
    }


    public void OnCrouch(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            crouchEvent?.Invoke();
        }
    }

    public void OnJump(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            jumpEvent?.Invoke();
        }
    }

    public void OnLook(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        mouseEvent?.Invoke(context.ReadValue<Vector2>());
    }

    public void OnMovement(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        movementEvent?.Invoke(context.ReadValue<Vector3>());
    }

    public void OnSprint(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            sprintPressEvent?.Invoke();
        }
        if (context.phase == InputActionPhase.Canceled)
        {
            sprintReleaseEvent?.Invoke();
        }
    }

    public void OnShoot(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            shootPressEvent?.Invoke();
        }
        if (context.phase == InputActionPhase.Canceled)
        {
            shootReleaseEvent?.Invoke();
        }
    }

    public void OnReload(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            reloadEvent?.Invoke();
        }
    }

    public void OnWeaponselection(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            weaponFirstPressEvent?.Invoke();
        }
        if (context.phase == InputActionPhase.Performed)
        {
            weaponSecondPressEvent?.Invoke();
        }
    }
}
