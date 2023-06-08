using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;
    public float rotationSpeed = 5f;

    public float smoothSpeed = 0.125f; // The smoothing speed

    public float zoomSpeed = 5f; // Zoom speed
    public float minZoomDistance = 2f; // Minimum zoom distance
    public float maxZoomDistance = 10f; // Maximum zoom distance

    private float currentZoom = 0f;
    private float currentYAngle = 0f;

    private void LateUpdate()
    {
        if (Input.GetMouseButton(1))
        {
            currentYAngle += Input.GetAxis("Mouse X") * rotationSpeed;
        }

        // Handle zoom
        float scrollDelta = Input.GetAxis("Mouse ScrollWheel");
        currentZoom -= scrollDelta * zoomSpeed;
        currentZoom = Mathf.Clamp(currentZoom, minZoomDistance, maxZoomDistance);
        Vector3 zoomOffset = offset.normalized * currentZoom;

        Vector3 desiredPosition = target.position + Quaternion.Euler(0, currentYAngle, 0) * zoomOffset;
        // Smoothly move the camera towards the desired position
        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        transform.LookAt(target);
    }
}
