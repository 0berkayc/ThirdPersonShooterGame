using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;           // Player objesi
    public Vector3 offset = new Vector3(0, 2, -4);
    public float rotationSpeed = 5f;

    private float yaw = 0f;
    private float pitch = 10f;

    void LateUpdate()
    {
        if (target == null) return; // Player atanmadıysa çık

        // Mouse input
        yaw += Input.GetAxis("Mouse X") * rotationSpeed;
        pitch -= Input.GetAxis("Mouse Y") * rotationSpeed;
        pitch = Mathf.Clamp(pitch, -20, 60);

        // Kamera rotasyonu
        Quaternion rotation = Quaternion.Euler(pitch, yaw, 0);
        transform.position = target.position + rotation * offset;
        transform.LookAt(target.position + Vector3.up * 1.5f);

        // Karakterin y ekseninde dönmesi (sadece yatay)
        Vector3 targetRotation = new Vector3(0, yaw, 0);
        target.rotation = Quaternion.Euler(targetRotation);
    }
}
