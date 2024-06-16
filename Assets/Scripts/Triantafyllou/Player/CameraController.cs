 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public InputReader reader;

    private Vector2 mouseMovement;

    public Camera cam;
    private float xRotation = 0f;
    private float yRotation = 0f;
    public float xSensitivity = 10f;
    public float ySensitivity = 10f;

    public float xRotationTopClamp = -90f;
    public float xRotationBotClamp = 90f;

    float mouseX;
    float mouseY;

    private void OnEnable()
    {
        reader.mouseEvent += mouseInput;
    }

    private void OnDisable()
    {
        reader.mouseEvent -= mouseInput;
    }

    void Start()
    {
        // Lock cursor to the midle of the screen and makes it invisible

        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
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

        //Apply rotations to transform
        cam.transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0f);

    }

    private void mouseInput(Vector2 input)
    {
        mouseMovement = input;
    }
}
