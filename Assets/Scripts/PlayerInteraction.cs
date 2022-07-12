using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteraction : MonoBehaviour
{
    public Camera playerCamera;

    public Vector3 interactionRayPoint = new Vector3(0.5f, 0.5f, 0f);
    public float interactionDistance = 5.0f;
    public LayerMask interactionLayer;

    private GameObject interactableObj;
    private bool interactionAvailabe = false;

    private InputManager inputManager;
    private InputAction interactControls;

    void Awake()
    {
        inputManager = new InputManager();
        interactControls = inputManager.Player.Interact;
    }

    void FixedUpdate()
    {
        interactionAvailabe = CheckInteraction();
        if (interactionAvailabe)
        {
            Debug.Log("FOCUS");
        }
    }

    bool CheckInteraction()
    {
        RaycastHit hit;
        Physics.Raycast(playerCamera.ViewportPointToRay(interactionRayPoint), out hit, interactionDistance, interactionLayer);

        if (hit.collider != null)
        {
            interactableObj = hit.collider.gameObject;
            return true;
        }
        else return false;
    }

    void HandleInteraction(InputAction.CallbackContext context)
    {
        if (interactionAvailabe)
        {
            Debug.Log("INTERACT");
        }

    }

    void OnEnable()
    {
        interactControls.Enable();
        interactControls.performed += HandleInteraction;
    }

    void OnDisable()
    {
        interactControls.Disable();
    }

}
