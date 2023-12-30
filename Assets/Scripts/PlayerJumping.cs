using UnityEngine;

public class PlayerJumping : MonoBehaviour
{
  public bool active = true;
  public float jumpHeight = 7f;
  public float jumpHeightMultiplier = 1f;
  public float gravityScaleJump = 5f;
  private float gravityScaleFall;
  public float jumpCancelFalloff = 2f;
  public float coyoteTime = 0.1f;
  public float jumpBufferTime = 0.1f;
  private float coyoteTimer;
  private float jumpBufferTimer;
  private Rigidbody2D body;
  public bool isJumping;

  void Start()
  {
    body = GetComponent<Rigidbody2D>();
    gravityScaleFall = body.gravityScale;
  }

  void Update()
  {
    if (IsGrounded())
    {
      coyoteTimer = coyoteTime;
    }
    else
    {
      coyoteTimer -= Time.deltaTime;
    }

    if (Input.GetKeyDown(KeyCode.Space))
    {
      jumpBufferTimer = jumpBufferTime;
    }
    else
    {
      jumpBufferTimer -= Time.deltaTime;
    }

    // Jump if space is pressed and player is grounded
    if (jumpBufferTimer > 0f && coyoteTimer > 0f)
    {
      jumpBufferTimer = 0f;
      body.gravityScale = gravityScaleJump;
      float jumpForce = Mathf.Sqrt(jumpHeight * jumpHeightMultiplier * (Physics2D.gravity.y * body.gravityScale) * -2) * body.mass;
      isJumping = true;
      body.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }

    // Cancel jump if space is released
    if (isJumping)
    {
      if (Input.GetKeyUp(KeyCode.Space))
      {
        body.gravityScale = gravityScaleFall * jumpCancelFalloff;
        coyoteTimer = 0;
      }

      if (body.velocity.y < 0)
      {
        body.gravityScale = gravityScaleFall;
        isJumping = false;
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
    return isJumping;
  }
}
