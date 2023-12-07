using UnityEngine;

// Camera follows the player with specified offset and delay
public class CameraFollow : MonoBehaviour
{
  public Transform target;
  public Vector2 offset;
  public float lookAheadX = 4;

  // How long it takes for the camera to catch up to the player
  public float PosSmoothTime = 0.25f;

  private Vector3 velocity = Vector3.zero;

  void FixedUpdate()
  {
    float playerDirection = GameObject.Find("Player").GetComponent<Movement>().GetDirectionX();

    float x = target.position.x + offset.x + lookAheadX * playerDirection;
    float y = target.position.y + offset.y;
    float z = transform.position.z;

    Vector3 targetPosition = new(x, y, z);

    transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, PosSmoothTime);
  }
}
