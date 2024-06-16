using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JohnPC : MonoBehaviour
{
    public float speed = 5.0f;
    public float lookSensitivity = 2.0f;
    public float maxLookX = 60.0f;
    public float minLookX = -60.0f;
    public float crouchHeight = 1.0f;
    public float standingHeight = 2.0f;
    public float crouchTransitionSpeed = 5.0f;
    public float playerHeight = 2.0f;

    private Camera playerCamera;
    private CapsuleCollider playerCollider;
    private float rotationX = 0f;
    private bool isCrouched = false;
    private Vector3 originalCameraPosition;

    void Start()
    {
        playerCamera = GetComponentInChildren<Camera>();
        playerCollider = GetComponent<CapsuleCollider>();
        Cursor.lockState = CursorLockMode.Locked;
        originalCameraPosition = playerCamera.transform.localPosition;
    }

    void Update()
    {
        Move();
        Look();
        Crouch();

    }

    void Move()
    {
        float moveX = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        float moveZ = Input.GetAxis("Vertical") * speed * Time.deltaTime;

        Vector3 move = transform.right * moveX + transform.forward * moveZ;
        transform.position += move;
    }

    void Look()
    {
        float mouseX = Input.GetAxis("Mouse X") * lookSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * lookSensitivity;

        // Invert the vertical mouse look
        rotationX -= mouseY;
        rotationX = Mathf.Clamp(rotationX, minLookX, maxLookX);

        playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0f, 0f);
        transform.rotation *= Quaternion.Euler(0f, mouseX, 0f);
    }

    void Crouch()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            isCrouched = !isCrouched;

            if (isCrouched == true) speed = 2.5f;
            else speed = 5.0f;
        }

        float targetHeight = isCrouched ? crouchHeight : standingHeight;
        float currentHeight = Mathf.Lerp(playerCollider.height, targetHeight, Time.deltaTime * crouchTransitionSpeed);
        playerCollider.height = currentHeight;

        //re-centering the CapsuleCollider
        playerCollider.center = new Vector3(0, currentHeight / 2, 0);


        //adjusting camera relative to player height
        Vector3 cameraPosition = originalCameraPosition;
        cameraPosition.y = currentHeight - 0.5f;
        playerCamera.transform.localPosition = cameraPosition;


    }
}
