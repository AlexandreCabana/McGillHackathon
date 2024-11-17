using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform rocket; // Reference to the rocket's transform
    public float smoothSpeed = 0.125f; // Adjust for smoother or snappier movement
    public Vector3 offset; // Offset from the rocket's position

    void LateUpdate()
    {
        if (rocket == null) return;

        // Target position with offset
        Vector3 targetPosition = rocket.position + offset;

        // Smoothly interpolate the camera's position
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, targetPosition, smoothSpeed);

        // Apply the smoothed position
        transform.position = smoothedPosition;
    }
}