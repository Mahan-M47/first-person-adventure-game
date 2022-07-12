using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraLook : MonoBehaviour
{
    public float lookSensitivity = 10;
    public float minVerticalRotation = -80, maxVerticalRotation = 80;

    private InputManager inputManager;
    private InputAction lookControls;

    private float verticalRotation = 0, horizontalRotation = 0;

    void Awake()
    {
        inputManager = new InputManager();
        lookControls = inputManager.Player.Look;
    }

    void Update()
    {
        Vector2 lookDirecton = lookControls.ReadValue<Vector2>();
        moveCamera(lookDirecton);
    }

    void moveCamera(Vector2 lookDirecton)
    {
        horizontalRotation = lookDirecton.x * lookSensitivity * Time.deltaTime;
        verticalRotation  -= lookDirecton.y * lookSensitivity * Time.deltaTime;

        verticalRotation = Mathf.Clamp(verticalRotation, minVerticalRotation, maxVerticalRotation);
        transform.localRotation = Quaternion.Euler(verticalRotation, 0f, 0f);

        Transform playerTransform = transform.parent.transform;
        playerTransform.Rotate(Vector3.up * horizontalRotation);
    }

    void OnEnable()
    {
        lookControls.Enable();
    }

    void OnDisable()
    {
        lookControls.Disable();
    }
}

