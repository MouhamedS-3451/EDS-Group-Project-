using UnityEngine;

public class PlayerGroundDetection : MonoBehaviour
{
  [SerializeField] private LayerMask groundLayer;
  private bool isGrounded;

  public GameObject ground;

  // Sets isGrounded = true if colliding with Object that has Layer "Ground"
  private void OnTriggerStay2D(Collider2D collider)
  {
    if (collider != null && (((1 << collider.gameObject.layer) & groundLayer) != 0))
    {
      isGrounded = true;
      ground = collider.gameObject;
    }
  }

  private void OnTriggerExit2D(Collider2D collider)
  {
    if (collider != null && (((1 << collider.gameObject.layer) & groundLayer) != 0))
    {
      isGrounded = false;
      ground = null;
    }
  }

  public bool IsGrounded()
  {
    return isGrounded;
  }
}