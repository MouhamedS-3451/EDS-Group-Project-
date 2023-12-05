using UnityEngine;

public class PlayerGroundDetection : MonoBehaviour
{
  [SerializeField] private LayerMask groundLayer;
  private bool isGrounded;

  public GameObject ground;

  // Sets isGrounded = true if colliding with Object that has Layer "Ground"
  private void OnTriggerStay2D(Collider2D collider)
  {
    isGrounded = (collider != null && ((((1 << collider.gameObject.layer) & groundLayer) != 0)));
    if (isGrounded) ground = collider.gameObject;
  }

  private void OnTriggerExit2D(Collider2D collision)
  {
    isGrounded = false;
    ground = null;
  }

  public bool IsGrounded()
  {
    return isGrounded;
  }
}