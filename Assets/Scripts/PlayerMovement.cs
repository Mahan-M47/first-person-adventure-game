using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float speed = 10;
    public float sprintSpeed = 15;
    private bool isSprinting = false;

    public float gravity = -9.81f;
    private float speedY = 0;

    private InputManager inputManager;
    private InputAction moveControls, sprintControls, jumpControls;
    private Vector2 moveDirecton;

    private CharacterController characterController;

    void Awake()
    {
        inputManager = new InputManager();
        moveControls = inputManager.Player.Movement;
        jumpControls = inputManager.Player.Jump;
        sprintControls = inputManager.Player.Sprint;

        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        moveDirecton = moveControls.ReadValue<Vector2>().normalized;
        Move(moveDirecton);

        if (characterController.isGrounded)
        {
            speedY = 0;
        }
        else
        {
            speedY += gravity * Time.deltaTime;
            characterController.Move(Vector3.up * speedY * Time.deltaTime);
        }
    }

    void Move(Vector2 moveDirecton)
    {
        float currentSpeed = isSprinting ? sprintSpeed : speed;
        Vector3 move = transform.right * moveDirecton.x + transform.forward * moveDirecton.y;
        characterController.Move(move * currentSpeed * Time.deltaTime);
    }

    void Sprint(InputAction.CallbackContext context)
    {
        isSprinting = true;
    }

    void StopSprint(InputAction.CallbackContext context)
    {
        isSprinting = false;
    }

    void Jump(InputAction.CallbackContext context)
    {
        Debug.Log("JUMP");
    }

    void OnEnable()
    {
        moveControls.Enable();
        jumpControls.Enable();
        sprintControls.Enable();

        sprintControls.started += Sprint;
        sprintControls.canceled += StopSprint;
        jumpControls.performed += Jump;

    }

    void OnDisable()
    {
        moveControls.Disable();
        jumpControls.Disable();
        sprintControls.Disable();
    }
}
