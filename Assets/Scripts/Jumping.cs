using UnityEngine;

public class Jumping : MonoBehaviour
{
    public float jumpHeight = 7f;
    public float gravityScaleJump = 5f;
    public float gravityScaleFall = 15f;
    public float jumpCancelFalloff = 2f;
    private Rigidbody2D body;
    private bool jumping;
    
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {
            body.gravityScale = gravityScaleJump;
            float jumpForce = Mathf.Sqrt(jumpHeight * (Physics2D.gravity.y * body.gravityScale) * -2) * body.mass;
            jumping = true;
            body.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }

        if (jumping)
        {
            if (Input.GetKeyUp(KeyCode.Space))
            {
                body.gravityScale = gravityScaleFall*jumpCancelFalloff;
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
        return transform.Find("PlayerGroundCollider").GetComponent<GroundDetection>().IsGrounded();
    }
}
