using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField] private CharacterController controller;
    [SerializeField] private float speed = 200f;
    [SerializeField] private float gravity = -30f;
    private Vector3 verticalVelocity;
    private Vector2 horizontalInput;

    public void ReceiveInput(Vector2 _horizontalInput)
    {
        horizontalInput = _horizontalInput;
    }

    private void FixedUpdate()
    {
        Vector3 horizontalVelocity = transform.right * horizontalInput.x + transform.forward * horizontalInput.y;
        horizontalVelocity *= speed;
        controller.Move(horizontalVelocity * Time.deltaTime);

        verticalVelocity.y += gravity * Time.deltaTime;
        controller.Move(verticalVelocity * Time.deltaTime);
    }

    private void Update()
    {
        if (controller.isGrounded && verticalVelocity.y < 0)
        {
            verticalVelocity.y = -2f;
        }
        //jumping
        if (Input.GetButtonDown("Jump") && controller.isGrounded)
        {
            verticalVelocity.y = Mathf.Sqrt(2 * -gravity * 3.0f);
        }
    }
}