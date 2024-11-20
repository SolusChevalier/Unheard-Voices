using UnityEngine;

public class InputManager : MonoBehaviour
{
    private CharacterControls controls;
    private CharacterControls.GroundMovementActions groundMovement;
    private Vector2 horizontalInput;
    private Vector2 mouseInput;
    [SerializeField] private CharacterMovement movement;
    [SerializeField] private MouseLook mouseLook;

    private void Awake()
    {
        controls = new CharacterControls();
        groundMovement = controls.GroundMovement;
        groundMovement.HorizontalMovement.performed += ctx => horizontalInput = ctx.ReadValue<Vector2>();
        groundMovement.MouseX.performed += ctx => mouseInput.x = ctx.ReadValue<float>();
        groundMovement.MouseY.performed += ctx => mouseInput.y = ctx.ReadValue<float>();
    }

    private void Update()
    {
        Vector2 adjustedInput = new Vector2(horizontalInput.x * 0.5f, horizontalInput.y); // Halving the left/right strafe speed
        movement.ReceiveInput(adjustedInput);
        mouseLook.ReceiveInput(mouseInput);
    }

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }
}