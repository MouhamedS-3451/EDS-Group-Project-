using UnityEngine;
using UnityEngine.Rendering.Universal;

// Camera follows the player with specified offset and delay
public class CameraFollow : MonoBehaviour
{
  public Transform target;
  public Vector2 offset;
  public float lookAheadX = 4;

  // How long it takes for the camera to catch up to the player
  public float PosSmoothTime = 0.25f;
  public bool inCave = false;

  private Vector3 velocity = Vector3.zero;

  void FixedUpdate()
  {
    if (inCave)
    {
      transform.GetChild(0).gameObject.SetActive(transform.position.y > 175);
      transform.GetChild(1).gameObject.SetActive(transform.position.y > 175);
      transform.GetChild(3).gameObject.SetActive(transform.position.y <= 175);
    }

    float playerDirection = GameObject.Find("Player").GetComponent<PlayerMovement>().GetDirectionX();

    float x = target.position.x + offset.x + lookAheadX * playerDirection;
    float y = target.position.y + offset.y;
    float z = transform.position.z;

    Vector3 targetPosition = new(x, y, z);

    transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, PosSmoothTime);
  }
}
