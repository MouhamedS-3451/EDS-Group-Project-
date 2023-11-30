using UnityEngine;

// Camera follows the player with specified offset and delay
public class CameraScript : MonoBehaviour
{
    public Transform target;
    public Vector2 offset;
    
    // How long it takes for the camera to catch up to the player
    public float smoothTime = 0.25f;

    private Vector3 velocity = Vector3.zero;

    void FixedUpdate()
    {
        Vector3 offset_3D = new Vector3(offset.x, offset.y, transform.position.z);
        Vector3 targetPosition = target.position + offset_3D;

        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
    }
}
