using UnityEngine;

public class CameraCollision : MonoBehaviour
{
    [SerializeField]
    public Transform playerTransform;

    public float cameraDistance = 5.0f;
    public float raycastOffset = 0.5f;
    public float minimumDistance = 1.0f;

    private void Update()
    {
        HandleCameraCollision();
    }

    private void HandleCameraCollision()
    {
        Vector3 dirFromPlayerToCamera = transform.position - playerTransform.position;
        dirFromPlayerToCamera.Normalize();

        // Start the ray a bit closer to the player to prevent minor clipping issues
        Vector3 raycastStart = playerTransform.position + dirFromPlayerToCamera * raycastOffset;
        RaycastHit hit;

        // Cast the ray
        if (Physics.Raycast(raycastStart, dirFromPlayerToCamera, out hit, cameraDistance))
        {
            // If we hit something, adjust the camera position to just in front of the hit point
            transform.position = hit.point - dirFromPlayerToCamera * 0.2f;
        }
        else
        {
            // If we didn't hit anything, position the camera at the desired distance from the player
            transform.position = playerTransform.position + dirFromPlayerToCamera * cameraDistance;
        }

        // Ensure the camera doesn't get too close to the player
        if (Vector3.Distance(transform.position, playerTransform.position) < minimumDistance)
        {
            transform.position = playerTransform.position + dirFromPlayerToCamera * minimumDistance;
        }
    }
}