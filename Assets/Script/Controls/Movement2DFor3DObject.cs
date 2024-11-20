using UnityEngine;
using UnityEngine.InputSystem;

public class Movement3D : MonoBehaviour, CharacterControls.I_2DMovementActions
{
    public float moveSpeed = 5f;
    private Vector2 moveInput;
    private Rigidbody rb;
    private CharacterControls characterControls;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        characterControls = new CharacterControls();
        characterControls._2DMovement.SetCallbacks(this);
    }

    public void On_2dMovers(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        // Movement on the X and Y axes
        rb.velocity = new Vector3(moveInput.x * moveSpeed, moveInput.y * moveSpeed, 0);
    }

    private void OnEnable()
    {
        characterControls.Enable();
    }

    private void OnDisable()
    {
        characterControls.Disable();
    }
}
