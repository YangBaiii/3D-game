using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target; // Set to Boss or map center
    public float distance = 20f;
    public float height = 20f; // Fixed height for top-down view
    public float rotationSpeed = 50f;
    private float currentAngle = 0f;

    void LateUpdate()
    {
        if (Input.GetKey(KeyCode.Q))
            currentAngle += rotationSpeed * Time.deltaTime;
        if (Input.GetKey(KeyCode.E))
            currentAngle -= rotationSpeed * Time.deltaTime;

        // Calculate position based on a fixed height and distance
        Quaternion rot = Quaternion.Euler(0, currentAngle, 0);
        Vector3 pos = target.position + rot * Vector3.back * distance + Vector3.up * height;
        transform.position = pos;
        transform.LookAt(target.position);
    }
} 