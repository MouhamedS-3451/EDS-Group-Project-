using UnityEngine;

public class Jumping : MonoBehaviour
{
  public bool active = true;
  public float jumpHeight = 7f;
  public float jumpHeightMultiplier = 1f;
  public float gravityScaleJump = 5f;
  private float gravityScaleFall;
  public float jumpCancelFalloff = 2f;
  private Rigidbody2D body;
  private bool jumping;

  void Start()
  {
    body = GetComponent<Rigidbody2D>();
    gravityScaleFall = body.gravityScale;
  }

  void Update()
  {
    //Debug.Log(jumping);
    // Jump if space is pressed and player is grounded
    if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
    {
      body.gravityScale = gravityScaleJump;
      float jumpForce = Mathf.Sqrt(jumpHeight * jumpHeightMultiplier * (Physics2D.gravity.y * body.gravityScale) * -2) * body.mass;
      jumping = true;
      body.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }

    // Cancel jump if space is released
    if (jumping)
    {
      if (Input.GetKeyUp(KeyCode.Space))
      {
        body.gravityScale = gravityScaleFall * jumpCancelFalloff;
      }

      if (body.velocity.y < 0)
      {
        body.gravityScale = gravityScaleFall;
        jumping = false;
      }
    }
  }

  private bool IsGrounded()
  {
    if (!active) return false;
    return transform.GetComponentInChildren<PlayerGroundDetection>().IsGrounded();
  }

  public bool IsJumping()
  {
    return jumping;
  }
}
